using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Compiler.Core.Syntax.AL
{
    public class CodeUnitSyntax : ObjectSyntax
    {
        List<SyntaxMember> members;
        ClassDeclarationSyntax classDeclaration;

        public CodeUnitSyntax()
        {
            members = new List<SyntaxMember>();
        }
        private CodeUnitSyntax(CodeUnitSyntax codeUnitSyntax) : this()
        {
            members = codeUnitSyntax.members;
            classDeclaration = codeUnitSyntax.classDeclaration;
        }

        public override bool TryParse(MemberDeclarationSyntax memberDeclaration,
            Func<MemberDeclarationSyntax, SyntaxMember> analyser, out SyntaxMember memberSyntax)
        {
            memberSyntax = null;

            if (memberDeclaration is ClassDeclarationSyntax classDeclaration)
            {
                if (!ContainsBaseType(classDeclaration.BaseList.Types))
                    return false;

                foreach (var member in classDeclaration.Members)
                    members.Add(analyser(member));

                this.classDeclaration = classDeclaration;
                memberSyntax = new CodeUnitSyntax(this);
                return true;
            }

            return false;

        }

        private bool ContainsBaseType(SeparatedSyntaxList<BaseTypeSyntax> types)
        {
            foreach (var type in types)
            {
                if (type.Type.ToString() != "NavCodeunit")
                    continue;

                return true;
            }

            return false;
        }
    }
}
