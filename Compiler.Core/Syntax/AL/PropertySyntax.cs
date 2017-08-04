using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.Core.Syntax.AL
{
    public class PropertySyntax : ALSourceMemberSyntax<PropertyDeclarationSyntax>
    {
        public PropertySyntax()
        {

        }
        private PropertySyntax(PropertySyntax propertySyntax)
        {
            CSharpMember = propertySyntax.CSharpMember;
        }

        public override bool TryParse(MemberDeclarationSyntax memberDeclaration, Func<MemberDeclarationSyntax, SyntaxMember> analyser, out SyntaxMember memberSyntax)
        {
            memberSyntax = null;

            if(memberDeclaration is PropertyDeclarationSyntax propertyDeclaration)
            {
                CSharpMember = propertyDeclaration;
                memberSyntax = new PropertySyntax(this);
                return true;
            }

            return false;
        }
    }
}
