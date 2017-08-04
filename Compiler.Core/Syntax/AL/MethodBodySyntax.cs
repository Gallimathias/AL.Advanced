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
            Func<MemberDeclarationSyntax, SyntaxMember> analyser, out SyntaxMember memberSyntax)
        {
            memberSyntax = null;

            if (memberDeclaration is ClassDeclarationSyntax classDeclaration)
            {
                if (classDeclaration.Parent is ClassDeclarationSyntax parentClass)
                {
                    if (!ContainsBaseType(classDeclaration.BaseList.Types, parentClass.Identifier.ToString()))
                        return false;
                }
                else
                {
                    return false;
                }

                CSharpMember = classDeclaration;
                memberSyntax = new MethodBodySyntax(this);
                return true;
            }

            return false;
        }

        private bool ContainsBaseType(SeparatedSyntaxList<BaseTypeSyntax> types, string parentIdentifier)
        {
            foreach (var type in types)
            {
                if (type.Type.ToString() != $"NavMethodScope<{parentIdentifier}>") //NavMethodScope<Codeunit93000>
                    continue;

                return true;
            }

            return false;
        }
    }
}
