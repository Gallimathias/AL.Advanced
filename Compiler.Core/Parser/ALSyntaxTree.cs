using System;
using Compiler.Core.Syntax;
using Compiler.Core.Syntax.AL;
using System.Collections.Generic;

namespace Compiler.Core.Parser
{
    [SyntaxElement(SyntaxSource.ALSource, SyntaxElement.Tree)]
    public class ALSyntaxTree : SyntaxTree
    {
        public ALSyntaxTree(List<SyntaxMember> memberSyntax) : base(memberSyntax)
        {
            SyntaxSource = SyntaxSource.ALSource;
        }

        public override SyntaxTree Parse(SyntaxTree syntaxTree)
        {
            throw new NotImplementedException();
        }
    }
}
