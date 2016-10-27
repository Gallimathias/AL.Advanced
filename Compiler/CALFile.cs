using System.Collections.Generic;
using System.IO;

namespace CAlConverter
{
    public class CALFile
    {
        private string path;
        private string tempSource;
        public FileInfo FileInfo { get; private set; }

        public CALFile(string path, string tempSource, List<CALObject> objects)
        {
            this.path = path;
            this.tempSource = tempSource;

            FileInfo = new FileInfo(path);
        }
    }
}