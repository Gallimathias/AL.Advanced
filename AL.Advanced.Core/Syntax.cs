using System;
using System.Collections.Generic;
using System.Text;

namespace AL.Advanced.Core
{
    public abstract class Syntax
    {
        protected Type nodeType;

        public abstract void Parse(object root);
        public abstract bool TryParse(object root);

        public abstract bool Check(object root);

        public abstract string ToText();
    }
    public abstract class Syntax<TNode> : Syntax
    {
        public Syntax()
        {
            nodeType = typeof(TNode);
        }

        public abstract void Parse(TNode root);
        public override void Parse(object root) => Parse((TNode)root);

        public abstract bool TryParse(TNode root);
        public override bool TryParse(object root) => TryParse((TNode)root);

        public abstract bool Check(TNode root);
        public override bool Check(object root) => Check((TNode)root);
    }
}
