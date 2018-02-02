using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nav.API
{
    public static class NavObjectHelper
    {
        public static string GetCode(NavObject @object)
        {
            var connection = new NavDatabaseContext();
            var meta = connection.ObjectMetadata.FirstOrDefault(
                m => m.ObjectID == @object.ID && m.ObjectType == @object.Type);

            return meta == null ? "" : GetStringFromBLOB(meta.UserCode);
        }


        private static int BlobMagic = 0x02457D5B;

        public static string GetStringFromBLOB(Binary value) => Encoding.UTF8.GetString(GetBlob(value));

        public static byte[] GetBlob(Binary value, bool compress = true)
        {
            var sourceArray = value.ToArray();

            using (var newStream = new MemoryStream())
            {
                using (var stream = new MemoryStream(sourceArray))
                {
                    byte[] array2 = new byte[4];
                    long num2 = stream.Read(array2, 0, 4);
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
                    byte[] array2 = BitConverter.GetBytes(BlobMagic);
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
