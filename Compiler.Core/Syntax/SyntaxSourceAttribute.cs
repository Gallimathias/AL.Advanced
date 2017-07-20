using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.Core.Syntax
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
    class SyntaxSourceAttribute : Attribute
    {
        public SyntaxSource SyntaxSource { get; private set; }

        public SyntaxSourceAttribute(SyntaxSource syntaxSource)
        {
            SyntaxSource = syntaxSource;
        }
    }
}
