using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Compiler.Core.Syntax.ALAdvanced.Members
{
    [SyntaxSource(SyntaxSource.ALAdvanced)]
    public abstract class ALAdvancedMemberSyntax<TCSharpMember> : SyntaxMember  where TCSharpMember : MemberDeclarationSyntax
    {
        public TCSharpMember CSharpMember { get; protected set; }

        internal override MemberDeclarationSyntax GetCSharpSyntax() => CSharpMember;

        public abstract override bool TryParse(MemberDeclarationSyntax memberDeclaration,
            Func<MemberDeclarationSyntax, SyntaxMember> analyser, out SyntaxMember memberSyntax);
        
    }
}
