using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Compiler.Core.Syntax.ALAdvanced
{
    [SyntaxSource(SyntaxSource.ALAdvanced)]
    public abstract class ALAdvancedSourceMemberSyntax<TCSharpMember> : SyntaxMember
    {
        public TCSharpMember CSharpMember { get; protected set; }

        public abstract override bool TryParse(MemberDeclarationSyntax memberDeclaration,
            Func<MemberDeclarationSyntax, SyntaxMember> analyser, out SyntaxMember memberSyntax);
        
    }
}
