using System;
using System.Collections.Generic;
using System.Text;

namespace AL.Advanced.Core.Definition.Global
{
    internal abstract class Method : Member
    {
        public List<Statement> Statements { get; set; }
    }
}
