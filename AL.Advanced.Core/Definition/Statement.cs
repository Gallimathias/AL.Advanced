using System;
using System.Collections.Generic;
using System.Text;

namespace AL.Advanced.Core.Definition
{
    public abstract class Statement : Syntax
    {

    }
    public abstract class Statement<TNode> : Statement
    {
        public Statement()
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
