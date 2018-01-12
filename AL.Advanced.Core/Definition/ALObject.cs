using System;
using System.Collections.Generic;
using System.Text;

namespace AL.Advanced.Core.Definition
{
    public abstract class ALObject<TNode> : Member<TNode>
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public ObjectType ObjectType { get; set; }

        public List<Member<TNode>> Members { get; set; }

        public override string ToString() => $"{ObjectType.ToString()} {ID} {Name}";
    }
}
