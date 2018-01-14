using System;
using System.Collections.Generic;
using System.Text;

namespace AL.Advanced.Core
{
    public abstract class Tree
    {
        protected Type childeType;

        public UnitRoot UnitRoot { get; protected set; }

        protected Tree(UnitRoot unitRoot)
        {
            UnitRoot = unitRoot;
        }

        public virtual string ToText() => UnitRoot.ToText().Trim();

        public virtual T Convert<T>() where T : Tree
        {
            var target = (T)Activator.CreateInstance(typeof(T), new UnitRoot());
            target.CopyRoot(UnitRoot);

            return target;
        }

        public abstract void CopyRoot(UnitRoot unitRoot);

    }
}
