using System;
using System.Collections.Generic;
using System.Text;

namespace AL.Advanced.Core.Definition
{
    public abstract class ALObject : Member
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public ObjectType ObjectType { get; set; }
        public List<Member> Members { get; set; }

        public override string ToString() => $"{ObjectType.ToString()} {ID} {Name}";

        public override T GetCopyAs<T>()
        {
            var props = typeof(T).GetProperties();

            var newObject = Activator.CreateInstance<T>();

            foreach (var prop in props)
            {
                if (!prop.CanRead || !prop.CanWrite)
                    continue;

                prop.SetValue(newObject, prop.GetValue(this));
            }

            return newObject;
        }
    }

    public abstract class ALObject<TNode> : ALObject
    {
        public ALObject()
        {
            nodeType = typeof(TNode);
        }

        public abstract bool Check(TNode root);
        public override bool Check(object root) => Check((TNode)root);

        public abstract void Parse(TNode root);
        public override void Parse(object root) => Parse((TNode)root);

        public abstract bool TryParse(TNode root);
        public override bool TryParse(object root) => TryParse((TNode)root);
    }
}
