using System;
using System.Collections.Generic;
using System.Text;

namespace AL.Advanced.Core.Definition
{
    internal abstract class ALObject : Syntax
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public ObjectType ObjectType { get; set; }

        public List<Member> Members { get; set; }

        public override string ToString() => $"{ObjectType.ToString()} {ID} {Name}";
    }
}
