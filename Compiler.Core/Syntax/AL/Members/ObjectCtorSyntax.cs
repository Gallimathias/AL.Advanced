using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis;

namespace Compiler.Core.Syntax.AL.Members
{
    public class ObjectCtorSyntax : ALSourceMemberSyntax<ConstructorDeclarationSyntax>
    {
        public ObjectCtorSyntax()
        {

        }
        private ObjectCtorSyntax(ObjectCtorSyntax objectCtorSyntax)
        {
            CSharpMember = objectCtorSyntax.CSharpMember;
        }

        public override bool TryParse(MemberDeclarationSyntax memberDeclaration, Func<MemberDeclarationSyntax, SyntaxMember> analyser, out SyntaxMember memberSyntax)
        {
            memberSyntax = null;

            if(memberDeclaration is ConstructorDeclarationSyntax ctorDeclaration)
            {
                CSharpMember = ctorDeclaration;
                memberSyntax = new ObjectCtorSyntax(this);
                return true;
            }

            return false;
        }

        internal override MemberDeclarationSyntax GetCSharpSyntax()
        {
            throw new NotImplementedException();
        }

        internal override void Normalize()
        {
            throw new NotImplementedException();
        }

        internal override void ParseCSharp()
        {
            throw new NotImplementedException();
        }
    }
}
