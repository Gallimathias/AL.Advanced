using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Compiler.Core.Syntax.AL.Members
{
    [SyntaxSource(SyntaxSource.ALSource)]
    public abstract class ALSourceMemberSyntax<TCSharpMember> : SyntaxMember where TCSharpMember : MemberDeclarationSyntax
    {
        public ALSourceMemberSyntax()
        {

        }


        internal override MemberDeclarationSyntax GetCSharpSyntax() => CSharpMember;

        public TCSharpMember CSharpMember { get; protected set; }
    }
}
