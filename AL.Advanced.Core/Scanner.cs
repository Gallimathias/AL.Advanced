using AL.Advanced.Core.Definition;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace AL.Advanced.Core
{
    public abstract class Scanner<TNode>
    {
        public Scanner()
        {
            Initialize();
        }

        public virtual void Initialize()
        {
        }

        public abstract bool TryScan(TNode member, out Member<TNode> root);
    }
}
