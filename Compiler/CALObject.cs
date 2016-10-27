using CoMaS;
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

        private CommandHandler<StringNode, bool> commandHandler;

        public CALObject()
        {
            commandHandler = new CommandHandler<StringNode, bool>();

            commandHandler["OBJECT-PROPERTIES"] += (s) => setProperties(s);
            
        }

        

        public CALObject(string source) :this()
        {

        }

        public CALObject(StringNode node) : this()
        {
            this.node = node;
        }

        public CALObject(StringNode node, string sourceText) : this(node)
        {
            var array = sourceText.Split().Where(s => !string.IsNullOrWhiteSpace(s)).ToArray();

            if (array[0] != "OBJECT")
                throw new Exception("This is not a Object");

            CALObjectType temp = new CALObjectType();

            if (Enum.TryParse(array[1], out temp))
                ObjectType = temp;
            else
                throw new Exception("Object can not be null");

            Number = int.Parse(array[2]);

            for (int i = 3; i < array.Length; i++)
                Name += array[i];

            Name = Name.Trim(' ');

            for (int i = 0; i < node.Elements.Length; i++)
                commandHandler.Dispatch(node.Elements[i], node.Children[i]);   
        }

        public override string ToString()
        {
            return $"{ObjectType} {Number} {Name}";
        }

        private bool setProperties(StringNode s)
        {

            var array = s.Text.Replace("\n", "").Replace("\r", "").Split(';');
            
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = array[i].TrimStart(' ');
                array[i] = array[i].TrimEnd(' ');
            }

            array = array.Where(c => !string.IsNullOrEmpty(c)).ToArray();

            var time = array.FirstOrDefault(c => c.StartsWith("Time")).Split('=')[1];
            var date = array.FirstOrDefault(c => c.StartsWith("Date")).Split('=')[1];
            var version = array.FirstOrDefault(c => c.StartsWith("Version List")).Split('=')[1];

            Date = DateTime.Parse($"{date} {time}");
            VersionList = version;

            return true;
        }
    }
}
