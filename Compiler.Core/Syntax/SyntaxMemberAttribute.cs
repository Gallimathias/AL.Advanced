using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.Core.Syntax
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
    public class SyntaxMemberAttribute : Attribute
    {
        public SyntaxSource SyntaxSource { get; private set; }
        public SyntaxMember SyntaxMember { get; private set; }

        public SyntaxMemberAttribute(SyntaxSource syntaxSource, SyntaxMember syntaxMember)
        {
            SyntaxSource = syntaxSource;
            SyntaxMember = syntaxMember;
        }
    }
}
