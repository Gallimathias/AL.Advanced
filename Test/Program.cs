using Compiler.Core;
using Compiler.Core.AL;
using Microsoft.Build.Construction;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Editing;
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

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            var con = new DatabaseOne();
            var id = 90001;
            ObjectType type = ObjectType.CodeUnit;
            var folder = "examples";
            var meta = con.GetTable<Object_Metadata>().FirstOrDefault(m => m.Object_ID == id && m.Object_Type == (int)type);
            var obj = con.GetTable<Object>().FirstOrDefault(m => m.ID == id && m.Type == (int)type);

            var str = GetStringFromBLOB(meta.User_Code);
            File.Delete($@"C:\Temp\{folder}\{(int)type}_{obj.Name}.cs");
            using (var writer = new StreamWriter(File.OpenWrite($@"C:\Temp\{folder}\{(int)type}_{obj.Name}.cs")))
            {
                writer.Write(str);
            }

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
                    //using (DeflateStream deflateStream = new DeflateStream(stream, CompressionMode.Decompress))
                    //    deflateStream.CopyTo(newStream);
                    stream.CopyTo(newStream);
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
