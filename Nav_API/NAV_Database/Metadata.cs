using Nav_API.Interfaces;
using System.Data.Linq;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Text;

namespace Nav_API.NAV_Database
{
    internal class Metadata
    {
        public string SourceCode => GetStringFromBLOB(metadata.User_Code);
        public string ALCode => GetStringFromBLOB(metadata.User_AL_Code);

        private IObjectMetadata metadata;


        public Metadata(IObjectMetadata object_metadata)
        {
            metadata = object_metadata;
        }

        public string GetStringFromBLOB(Binary value)
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
    }
}