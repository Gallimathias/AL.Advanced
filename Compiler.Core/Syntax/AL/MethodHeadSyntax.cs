using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Compiler.Core.Syntax.AL
{
    public class MethodHeadSyntax : ALSourceMemberSyntax<MethodDeclarationSyntax>
    {

        public MethodHeadSyntax()
        {

        }
        private MethodHeadSyntax(MethodHeadSyntax methodHeadSyntax)
        {
            CSharpMember = methodHeadSyntax.CSharpMember;
        }

        public override bool TryParse(MemberDeclarationSyntax memberDeclaration,
            Func<MemberDeclarationSyntax, MemberSyntax> analyser, out MemberSyntax memberSyntax)
        {
            memberSyntax = null;

            if(memberDeclaration is MethodDeclarationSyntax methodDeclaration)
            {
                CSharpMember = methodDeclaration;
                memberSyntax = new MethodHeadSyntax(this);
                return true;
            }

            return false;
        }
    }
}
