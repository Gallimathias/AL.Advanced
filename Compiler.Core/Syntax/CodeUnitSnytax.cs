using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Compiler.Core.Syntax
{
    class CodeUnitSnytax : ObjectSyntax
    {
        List<MemberSyntax> members;
        public CodeUnitSnytax()
        {
            members = new List<MemberSyntax>();
        }
        private CodeUnitSnytax(CodeUnitSnytax origin) : this()
        {
            members = origin.members;
        }

        public override bool TryParse(MemberDeclarationSyntax memberDeclaration,
            Func<MemberDeclarationSyntax, MemberSyntax> analyser, out MemberSyntax memberSyntax)
        {
            memberSyntax = null;

            if (memberDeclaration is ClassDeclarationSyntax csharpClass)
            {
                if (!ContainsBaseType(csharpClass.BaseList.Types))
                    return false;

                foreach (var member in csharpClass.Members)
                    members.Add(analyser(member));

                memberSyntax = new CodeUnitSnytax(this);
                return true;
            }

            return false;

        }

        private bool ContainsBaseType(SeparatedSyntaxList<BaseTypeSyntax> types)
        {
            foreach (var type in types)
            {
                if (((IdentifierNameSyntax)type.Type).Identifier.Text != "NavCodeunit")
                    continue;

                return true;
            }

            return false;
        }
    }
}
