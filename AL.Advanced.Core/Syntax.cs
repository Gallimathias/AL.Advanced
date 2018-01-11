using System;
using System.Collections.Generic;
using System.Text;

namespace AL.Advanced.Core
{
    public abstract class Syntax<TNode>
    {
        public abstract void Parse(TNode root);
        public abstract void TryParse(TNode root);

        public abstract bool Check(TNode root);

        public abstract string ToText();
    }
}
