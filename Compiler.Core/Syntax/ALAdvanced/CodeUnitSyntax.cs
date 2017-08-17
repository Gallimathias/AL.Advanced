using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace Compiler.Core.Syntax.ALAdvanced
{
    public class CodeUnitSyntax : ObjectSyntax
    {
        public string Name { get; set; }
        public SyntaxKind Keyword { get; set; }

        List<SyntaxMember> members;

        public CodeUnitSyntax()
        {
            members = new List<SyntaxMember>();
        }
        private CodeUnitSyntax(CodeUnitSyntax codeUnitSyntax) : this()
        {
            members = codeUnitSyntax.members;
            CSharpMember = codeUnitSyntax.CSharpMember;
        }

       

        public override bool TryParse(MemberDeclarationSyntax memberDeclaration,
            Func<MemberDeclarationSyntax, SyntaxMember> analyser, out SyntaxMember memberSyntax)
        {
            memberSyntax = null;

            if(memberDeclaration is ClassDeclarationSyntax classDeclaration)
            {
                if (!ContainsBaseType(classDeclaration.BaseList.Types))
                    return false;

                foreach (var member in classDeclaration.Members)
                    members.Add(analyser(member));

                CSharpMember = classDeclaration;
                memberSyntax = new CodeUnitSyntax(this);
                return true;
            }

            return false;
        }

        internal override void ParseCSharp()
        {
            var tmp = new List<MemberDeclarationSyntax>();
            foreach (var member in members)
            {
                member.ParseCSharp();
                tmp.Add(member.GetCSharpSyntax());
            }


            CSharpMember = SyntaxFactory.ClassDeclaration(Name)
                .AddModifiers(SyntaxFactory.Token(Keyword))
                .AddBaseListTypes(SyntaxFactory.SimpleBaseType(SyntaxFactory.ParseTypeName("Codeunit")))
                .AddMembers(tmp.ToArray());
        }

        internal override MemberDeclarationSyntax GetCSharpSyntax() => CSharpMember;

        private bool ContainsBaseType(SeparatedSyntaxList<BaseTypeSyntax> types)
        {
            foreach (var type in types)
            {
                if (type.Type.ToString() == "Codeunit" || type.Type.ToString() != "AL.Codeunit")
                    return true;
            }

            return false;
        }
    }
}
