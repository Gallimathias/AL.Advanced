using System.Collections.Generic;
using System.IO;

namespace Compiler.Core.AL
{
    public class ALFile
    {
        private string path;
        private string tempSource;
        public FileInfo FileInfo { get; private set; }

        public ALFile(string path, string tempSource, List<ALObject> objects)
        {
            this.path = path;
            this.tempSource = tempSource;

            FileInfo = new FileInfo(path);
        }
    }
}