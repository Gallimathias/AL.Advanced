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
            var id = 90000;
            var type = 5;
            var folder = "examples";
            var meta = con.GetTable<Object_Metadata>().FirstOrDefault(m => m.Object_ID == id && m.Object_Type == type);
            var obj = con.GetTable<Object>().FirstOrDefault(m => m.ID == id && m.Type == type);
            
            var str = GetStringFromBLOB(meta.User_Code);
            File.Delete($@"C:\Temp\{folder}\{type.ToString()}_{obj.Name}.cs");
            using (var writer = new StreamWriter(File.OpenWrite($@"C:\Temp\{folder}\{type.ToString()}_{obj.Name}.cs")))
            {
                writer.WriteLine(str);
            }            
        }

        public static string GetStringFromBLOB(Binary value)
        {
            var sourceArray = value.ToArray();

            using (var newStream = new MemoryStream())
            {
                using (var stream = new MemoryStream(sourceArray))
                {
                    byte[] array2 = new byte[4];
                    long num2 = stream.Read(array2, 0, 4);

                    using (DeflateStream deflateStream = new DeflateStream(stream, CompressionMode.Decompress))
                        deflateStream.CopyTo(newStream);
                }

                return Encoding.UTF8.GetString(newStream.ToArray());
            }
        }

        public static Binary ToBLOB(string value)
        {

            var sourceArray = Encoding.UTF8.GetBytes(value);

            using (var newStream = new MemoryStream(sourceArray))
            {
                using (var stream = new MemoryStream())
                {
                    byte[] array2 = new byte[] { 2, 69, 125, 91 };
                    stream.Write(array2, 0, 4);

                    using (DeflateStream deflateStream = new DeflateStream(stream, CompressionMode.Compress))
                        newStream.CopyTo(deflateStream);

                    return new Binary(stream.ToArray());
                }


            }
        }
    }
}
