using System;
using System.Collections.Generic;
using System.Text;

namespace AL.Advanced.Core
{
    public abstract class Syntax
    {
        public abstract void Parse();
        public abstract void TryParse();

        public abstract bool Check();

        public abstract string AsString();
    }
}
