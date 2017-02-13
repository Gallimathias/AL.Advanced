using Compiler.Core.Help;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.Core.AL
{
    public static class ALSerializer
    {
        public static ALFile Deserialize(string path)
        {
            string tempSource;
            //List<ALObject> objects = new List<ALObject>();

            using (var reader = new StreamReader(path))
                tempSource = reader.ReadToEnd();

            var tree = StringHelper.ParseElementTree(tempSource, '{', '}');
            int lastI = 0;
            for (int i = 0; i < tree.BaseNode.Children.Count; i++)
            {
                var clear = tree.BaseNode.ClearText;
                int length = 0;
                if (clear.Split().Where(s => s == "OBJECT").Count() > 1)
                {
                    lastI = clear.IndexOf("OBJECT", lastI);
                    length = clear.IndexOf("OBJECT", lastI) - lastI;
                }
                else
                {
                    length = clear.Length;
                }

                //objects.Add(new ALObject(tree.BaseNode.Children[i],
                //    clear.Substring(lastI, length)));
            }



            return new ALFile();//path, tempSource, objects);
        }
    }
}
