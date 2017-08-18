using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis;

namespace Compiler.Core.Syntax.AL
{

    public class MethodBodySyntax : ObjectSyntax
    {
        public MethodBodySyntax()
        {

        }

        public string Identifier { get; internal set; }

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

                foreach (var member in classDeclaration.Members)
                    AddMember(analyser(member));

                

                memberSyntax = new MethodBodySyntax
                {
                    CSharpMember = classDeclaration,
                    members = members,
                    Identifier = (string)classDeclaration.Identifier.Value

                };
                return true;
            }

            return false;
        }


        internal override void Normalize()
        {
            throw new NotImplementedException();
        }

        internal override void ParseCSharp()
        {
            throw new NotImplementedException();
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
