using System;
using System.Collections.Generic;
using System.Text;

namespace AL.Advanced.Core
{
    public abstract class Tree<TChildType>
    {
        public UnitRoot UnitRoot { get; protected set; }
        
        protected Tree(UnitRoot unitRoot)
        {
            UnitRoot = unitRoot;
        }

        public virtual string ToText() => UnitRoot.ToText().Trim();
    }
}
