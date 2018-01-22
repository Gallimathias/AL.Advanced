using AL.Advanced.Core;
using AL.Advanced.Core.Definition;
using System;
using System.Collections.Generic;
using System.Text;

namespace AL.Advanced.Definition.AL
{
    class ALScanner : Scanner
    {
        public ALScanner()
        {
        }

        public override bool TryGetCopy(object member, out Member copie)
        {
            throw new NotImplementedException();
        }

        public override bool TryScan(object member, out Member root)
        {
            throw new NotImplementedException();
        }
    }
}
