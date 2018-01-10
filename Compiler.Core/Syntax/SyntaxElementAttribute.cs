using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.Core.Syntax
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
    public class SyntaxElementAttribute : Attribute
    {
        public SyntaxSource SyntaxSource { get; private set; }
        public SyntaxElement SyntaxMember { get; private set; }

        public SyntaxElementAttribute(SyntaxSource syntaxSource, SyntaxElement syntaxMember)
        {
            SyntaxSource = syntaxSource;
            SyntaxMember = syntaxMember;
        }
    }
}
