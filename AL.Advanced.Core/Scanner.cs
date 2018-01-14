using AL.Advanced.Core.Definition;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace AL.Advanced.Core
{
    public abstract class Scanner
    {
        protected Type nodeType;

        public Scanner()
        {
            Initialize();
        }

        public virtual void Initialize()
        {
        }

        public abstract bool TryScan(object member, out Member root);

        public abstract bool TryGetCopy(object member, out Member copie);
    }
    public abstract class Scanner<TNode> : Scanner
    {
        public Scanner()
        {
            nodeType = typeof(TNode);
        }

        public abstract bool TryScan(TNode member, out Member<TNode> root);

        public override bool TryScan(object member, out Member root)
        {
            var value = TryScan((TNode)member, out Member<TNode> result);
            root = result;
            return value;
        }

        public abstract bool TryGetCopy(TNode member, out Member<TNode> copie);

        public override bool TryGetCopy(object member, out Member copie)
        {
            var value = TryGetCopy((TNode)member, out Member<TNode> result);
            copie = result;
            return value;
        }
    }
}
