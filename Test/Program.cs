using Compiler.Core;
using System;
using System.Data.Linq;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using Test.IO;
using Test.IO.Nea;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {

            NavApps();
            return;
            //var stream = new FileStream(@"C:\Temp\NAV\C_Functions.fob", FileMode.Open,FileAccess.Read);
            //var encoded = NeaStreamReader.IsSupported(stream);

            //var reader = new NeaStreamReader(stream, true);

            //var import = new TxtImporter(TxtFileModelInfo.Instance);
            //var res = import.ImportFromStream(stream);

            var con = new DatabaseOne();
            var id = 96001;
            ObjectType type = ObjectType.CodeUnit;
            var folder = "examples";
            //var t = new string[6];
            //var obj = con.GetTable<NAV_App_Object_Metadata>().FirstOrDefault(o => o.Object_ID == id && o.Object_Type == (int)type);
            //var pck = con.GetTable<NAV_App>().FirstOrDefault();
            //var str = GetStringFromBLOB(obj.User_AL_Code);
            //var code = GetStringFromBLOB(obj.User_Code);

            //var pack = GetStringFromBLOB(pck.Blob);
            //var a = 12;
            var meta = con.GetTable<Object_Metadata>().FirstOrDefault(m => m.Object_ID == id && m.Object_Type == (int)type);
            var obj = con.GetTable<Object>().FirstOrDefault(m => m.ID == id && m.Type == (int)type);

            //File.WriteAllText("test", "test", Encoding.UTF8);

            var str = GetStringFromBLOB(meta.User_Code);
            //var code = GetCodeFromBLOB(obj.BLOB_Reference);
            //var metaData = GetStringFromBLOB(meta.Metadata);

            //var str2 = GetStringFromBLOB(File.ReadAllBytes(@"C:\Users\BID01023\Desktop\Empty.fob"));
            //var code2 = GetCodeFromBLOB(File.ReadAllBytes(@"C:\Users\BID01023\Desktop\Empty.fob"));
            File.Delete($@"C:\Temp\{folder}\{(int)type}_{obj.Name}.cs");
            using (var writer = new StreamWriter(File.OpenWrite($@"C:\Temp\{folder}\{(int)type}_{obj.Name}.cs")))
            {
                writer.Write(str);
            }

            //obj.BLOB_Reference = null;
            //obj.BLOB_Size = 0;
            //meta.User_Code = ToBLOB(File.ReadAllText(@"C:\Temp\test\5_Convert_Experimental.cs"));

            //con.SubmitChanges();
            //return;
            //var a = GetStringFromBLOB(obj.BLOB_Reference);
            //var b = Encoding.GetEncoding(1252).GetString(obj.BLOB_Reference.ToArray());
            //var c = Encoding.GetEncoding("Latin1").GetString(obj.BLOB_Reference.ToArray());

            //using (var binReader = new BinaryReader(new MemoryStream(DecompressBlob(obj.BLOB_Reference.ToArray()))))
            //{
            //    binReader.ReadByte();
            //    int count = binReader.ReadInt32();
            //    var stream = new MemoryStream(binReader.ReadBytes(count));
            //    var o = new BinaryFormatter().Deserialize(stream);
            //}
            Console.WriteLine("Export is finished");
            //Console.ReadKey();
        }

        private static void NavApps()
        {
            var con = new DatabaseOne();
            var folder = "nav_app";

            //var meta = con.GetTable<Object_Metadata>().FirstOrDefault(m => m.Object_ID == id && m.Object_Type == (int)type);
            //var obj = con.GetTable<Object>().FirstOrDefault(m => m.ID == id && m.Type == (int)type);

            var app = con.GetTable<NAV_App>().FirstOrDefault(n => n.ID == new Guid("186E76FE-79D3-4AEB-A7AC-078B66FC258D"));

            //var str = GetStringFromBLOB(meta.User_Code);
            //File.Delete($@"C:\Temp\{folder}\{app.Name}.app");
            //using (var writer = new BinaryWriter(File.OpenWrite($@"C:\Temp\{folder}\{app.Name}.app")))
            //{
            //    writer.Write(GetBlob(app.Blob));
            //}

            byte[] data;

            using (var reader = new BinaryReader(File.OpenRead($@"C:\Temp\{folder}\{app.Name}.app")))
            {
                data = reader.ReadBytes((int)reader.BaseStream.Length);
            }

            app.Blob = ToBLOB(data);
            con.SubmitChanges();
        }

        private static string GetFromBLOB(Binary bLOB_Reference)
        {
            var data = GetBlob(bLOB_Reference);// bLOB_Reference.ToArray();

            var stream = new Rc4Stream(new MemoryStream(data), NeaStream.Key, true);
            var buffer = new byte[data.Length];
            var count = stream.Read(buffer, 0, (int)stream.Length);

            File.WriteAllBytes(@"C:\temp\test.rc4", buffer);


            //using (var reader = new StreamReader(stream, true))//
            //{
            //    var code = reader.ReadToEnd();
            //}
            var res = Encoding.GetEncoding(437).GetString(buffer);
            return res; //Encoding.UTF8.GetString(data);
        }

        private static int BlobMagic = 0x02457D5B;

        public static string GetStringFromBLOB(Binary value) => Encoding.GetEncoding(1252).GetString(GetBlob(value));

        public static byte[] GetBlob(Binary value, bool compress = true)
        {
            var sourceArray = value.ToArray();

            using (var newStream = new MemoryStream())
            {
                using (var stream = new MemoryStream(sourceArray))
                {
                    byte[] array2 = new byte[4];
                    long num2 = stream.Read(array2, 0, 4);
                    //if (BitConverter.ToInt32(array2, 0) != BlobMagic)
                    //    throw new NotSupportedException("Wrong magic");
                    if (compress)
                    {
                        using (DeflateStream deflateStream = new DeflateStream(stream, CompressionMode.Decompress))
                            deflateStream.CopyTo(newStream);
                    }
                    else
                    {
                        var buffer = new byte[stream.Length - stream.Position];
                        stream.Read(buffer, 0, buffer.Length);
                        newStream.Write(buffer, 0, buffer.Length);
                    }
                    //stream.CopyTo(newStream);
                }

                return newStream.ToArray();
            }
        }

        public static Binary ToBLOB(string value) => ToBLOB(Encoding.UTF8.GetBytes(value));
        public static Binary ToBLOB(byte[] data, bool compress = true)
        {
            var sourceArray = data;

            using (var newStream = new MemoryStream(sourceArray))
            {
                using (var stream = new MemoryStream())
                {
                    byte[] array2 = BitConverter.GetBytes(BlobMagic);// new byte[] { 2, 69, 125, 91 };
                    stream.Write(array2, 0, 4);

                    if (compress)
                    {
                        using (DeflateStream deflateStream = new DeflateStream(stream, CompressionMode.Compress))
                            newStream.CopyTo(deflateStream);
                    }
                    else
                    {
                        var buffer = new byte[stream.Length - stream.Position];
                        stream.Read(buffer, 0, buffer.Length);
                        newStream.Write(buffer, 0, buffer.Length);
                    }

                    return new Binary(stream.ToArray());
                }


            }
        }
    }
}
