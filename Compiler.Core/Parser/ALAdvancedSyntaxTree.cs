using Compiler.Core.Syntax;
using Compiler.Core.Syntax.ALAdvanced.Members;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.Core.Parser
{
    public class ALAdvancedSyntaxTree : SyntaxTree
    {
        public ALAdvancedSyntaxTree(SyntaxMember memberSyntax) : base(memberSyntax)
        {
        }

        public override SyntaxTree Parse(SyntaxTree syntaxTree)
        {
            throw new NotImplementedException();
        }

        public string ToFullString() => ((ObjectSyntax)RootMember).CSharpMember.ToFullString();
    }
}
