using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAlConverter
{
    public static class CALSerializer
    {
        public static CALFile Deserialize(string path)
        {
            string tempSource;
            List<CALObject> objects = new List<CALObject>();

            using (var reader = new StreamReader(path))
                tempSource = reader.ReadToEnd();

            var tree = StringHelper.ParseElementTree(tempSource, '{', '}');
            foreach (var node in tree.Nodes.Where(n => n.OrderNumber == 1))
                objects.Add(new CALObject(node));


            return new CALFile(path, tempSource, objects);
        }
    }
}
