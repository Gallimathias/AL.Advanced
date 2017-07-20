using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis;

namespace Compiler.Core.Syntax.AL
{

    public class MethodBodySyntax : ALSourceMemberSyntax<ClassDeclarationSyntax>
    {
        public MethodBodySyntax()
        {

        }
        private MethodBodySyntax(MethodBodySyntax methodBodySyntax)
        {
            CSharpMember = methodBodySyntax.CSharpMember;
        }

        public override bool TryParse(MemberDeclarationSyntax memberDeclaration,
            Func<MemberDeclarationSyntax, MemberSyntax> analyser, out MemberSyntax memberSyntax)
        {
            memberSyntax = null;

            if (memberDeclaration is ClassDeclarationSyntax classDeclaration)
            {
                if (!(classDeclaration.Parent is ClassDeclarationSyntax))
                    return false;

                if (!ContainsBaseType(classDeclaration.BaseList.Types))
                    return false;
                
                CSharpMember = classDeclaration;
                memberSyntax = new MethodBodySyntax(this);
                return true;
            }

            return false;
        }

        private bool ContainsBaseType(SeparatedSyntaxList<BaseTypeSyntax> types)
        {
            foreach (var type in types)
            {
                if (((IdentifierNameSyntax)type.Type).Identifier.Text != "NavMethodScope")
                    continue;

                return true;
            }

            return false;
        }
    }
}
