using AL.Advanced.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace AL.Advanced.Definition.AL
{
    internal class ALTree : Tree
    {
        protected ALTree(UnitRoot unitRoot) : base(unitRoot)
        {
        }

        public override void CopyRoot(UnitRoot unitRoot)
        {
            throw new NotImplementedException();
        }
    }
}
