using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAlConverter
{
    public class CALObject
    {
        private StringNode node;

        public CALObjectType ObjectType { get; set; }
        public string Name { get; set; }
        public int Number { get; set; }
        public DateTime Date { get; set; }
        public string VersionList { get; set; }
        public bool Modifyed { get; set; }
        public List<CALProperties> Properties { get; set; }
        public CALCode Code { get; set; }
        public string Documentation { get; set; }

        public CALObject(string source)
        {
            validate(source);
            Properties = getProperties(source);
            Code = getCode(source);
            Documentation = getDocumentation(source);

        }

        public CALObject(StringNode node)
        {
            this.node = node;
        }

        private string getDocumentation(string source)
        {
            throw new NotImplementedException();
        }

        private CALCode getCode(string source)
        {
            throw new NotImplementedException();
        }

        private List<CALProperties> getProperties(string source)
        {
            var start = source.Substring(source.IndexOf("PROPERTIES")).IndexOf('{');

            //var workString = source.Substring();
            throw new NotImplementedException();
        }

        private void validate(string source)
        {
            if (!source.StartsWith("OBJECT"))
                return;

            var subs = source.Substring(7, source.IndexOf('{') - 7).Split();
            ObjectType = (CALObjectType)Enum.Parse(typeof(CALObjectType), subs[0]);
            Number = int.Parse(subs[1]);

            for (int i = 2; i < subs.Length - 1; i++)
                Name += subs[i];

            subs = source.Substring(source.IndexOf("OBJECT - PROPERTIES") + 21, source.IndexOf('}')).Split(';');

            Date = DateTime.Parse(
                $"{subs.First(s => s.StartsWith("Date")).Split('=')[1]} {subs.First(s => s.StartsWith("Time")).Split('=')[1]}");

            VersionList = subs.First(s => s.StartsWith("Version List")).Split('=')[1];


        }
    }
}
