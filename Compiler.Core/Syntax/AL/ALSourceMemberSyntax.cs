using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Compiler.Core.Syntax.AL
{
    [SyntaxSource(SyntaxSource.ALSource)]
    public abstract class ALSourceMemberSyntax<TCSharpMember> : MemberSyntax
    {
        public ALSourceMemberSyntax()
        {

        }

        public TCSharpMember CSharpMember { get; protected set; }

        public abstract override bool TryParse(MemberDeclarationSyntax memberDeclaration, Func<MemberDeclarationSyntax, MemberSyntax> analyser, out MemberSyntax memberSyntax);
    }
}
