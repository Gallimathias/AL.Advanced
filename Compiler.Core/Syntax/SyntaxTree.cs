using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.Core.Syntax
{
    public abstract class SyntaxTree
    {
        public SyntaxStream SyntaxStream { get; protected set; }
        public MemberSyntax RootMember { get; set; }

        public SyntaxTree()
        {
            
        }

        internal static SyntaxTree GetTree(List<MemberSyntax> result, SyntaxSource syntaxSource)
        {

            throw new NotImplementedException();
        }
    }
}
