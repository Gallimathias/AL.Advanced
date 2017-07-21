using System;
using Compiler.Core.Syntax;
using Compiler.Core.Syntax.AL;

namespace Compiler.Core.Parser
{
    [SyntaxMember(SyntaxSource.ALSource, SyntaxMember.tree)]
    public class ALSyntaxTree : SyntaxTree
    {
        public ALSyntaxTree(MemberSyntax memberSyntax) : base(memberSyntax)
        {
            SyntaxSource = SyntaxSource.ALSource;
        }

        public override SyntaxTree Parse(SyntaxTree syntaxTree)
        {
            throw new NotImplementedException();
        }
    }
}
