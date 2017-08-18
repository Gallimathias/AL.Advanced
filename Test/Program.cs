using Compiler.Core;
using Compiler.Core.AL;
using Microsoft.Build.Construction;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Editing;
using Microsoft.Dynamics.Nav.Model.IO.Txt;
using Nav_API;
using Nav_API.NAV_Database;
using Nav_API.SQL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Linq;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Test.IO;
using Test.IO.Nea;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            //var stream = new FileStream(@"C:\Temp\NAV\C_Functions.fob", FileMode.Open,FileAccess.Read);
            //var encoded = NeaStreamReader.IsSupported(stream);

            //var reader = new NeaStreamReader(stream, true);

            //var import = new TxtImporter(TxtFileModelInfo.Instance);
            //var res = import.ImportFromStream(stream);

            var con = new DatabaseOne();
            var id = 1070;
            ObjectType type = ObjectType.CodeUnit;
            var folder = "examples";

            var obj = con.GetTable<NAV_App_Object_Metadata>().FirstOrDefault(o => o.Object_ID == id && o.Object_Type == (int)type);
            var pck = con.GetTable<NAV_App>().FirstOrDefault();
            var str = GetStringFromBLOB(obj.User_AL_Code);
            var code = GetStringFromBLOB(obj.User_Code);

            var pack = GetStringFromBLOB(pck.Blob);
            var a = 12;
            //var meta = con.GetTable<Object_Metadata>().FirstOrDefault(m => m.Object_ID == id && m.Object_Type == (int)type);
            //var obj = con.GetTable<Object>().FirstOrDefault(m => m.ID == id && m.Type == (int)type);



            File.WriteAllText("test", "test", Encoding.UTF8);

            //var str = GetStringFromBLOB(meta.User_Code);
            //var code = GetCodeFromBLOB(obj.BLOB_Reference);
            //File.Delete($@"C:\Temp\{folder}\{(int)type}_{obj.Name}.cs");
            //using (var writer = new StreamWriter(File.OpenWrite($@"C:\Temp\{folder}\{(int)type}_{obj.Name}.cs")))
            //{
            //    writer.Write(str);
            //}

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
            Console.ReadKey();
        }

        private static string GetCodeFromBLOB(Binary bLOB_Reference)
        {
            var data = DecompressBlob(bLOB_Reference);// bLOB_Reference.ToArray();

            var stream = new Rc4Stream(new MemoryStream(data), NeaStream.Key, true);
            var buffer = new byte[data.Length];
            var count = stream.Read(buffer, 0, (int)stream.Length);

            File.WriteAllBytes(@"C:\temp\test.rc4",buffer);
            

            //using (var reader = new StreamReader(stream, true))//
            //{
            //    var code = reader.ReadToEnd();
            //}
            var res = Encoding.GetEncoding(437).GetString(buffer);
            return res; //Encoding.UTF8.GetString(data);
        }

        private static int BlobMagic = 0x02457D5B;
        public static string GetStringFromBLOB(Binary value)
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
                    using (DeflateStream deflateStream = new DeflateStream(stream, CompressionMode.Decompress))
                        deflateStream.CopyTo(newStream);
                    //stream.CopyTo(newStream);
                }

                return Encoding.UTF8.GetString(newStream.ToArray());
            }
        }
        public static byte[] DecompressBlob(Binary value)
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
                    using (DeflateStream deflateStream = new DeflateStream(stream, CompressionMode.Decompress))
                        deflateStream.CopyTo(newStream);
                    //stream.CopyTo(newStream);
                }

                return newStream.ToArray();
            }
        }

        public static Binary ToBLOB(string value)
        {

            var sourceArray = Encoding.UTF8.GetBytes(value);

            using (var newStream = new MemoryStream(sourceArray))
            {
                using (var stream = new MemoryStream())
                {
                    byte[] array2 = BitConverter.GetBytes(BlobMagic);// new byte[] { 2, 69, 125, 91 };
                    stream.Write(array2, 0, 4);

                    using (DeflateStream deflateStream = new DeflateStream(stream, CompressionMode.Compress))
                        newStream.CopyTo(deflateStream);

                    return new Binary(stream.ToArray());
                }


            }
        }
    }
}
