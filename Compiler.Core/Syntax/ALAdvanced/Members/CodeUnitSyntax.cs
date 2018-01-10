using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace Compiler.Core.Syntax.ALAdvanced.Members
{
    public class CodeUnitSyntax : ObjectSyntax
    {       
       
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

                Modifier = classDeclaration.Modifiers.First().Kind();

                memberSyntax = new CodeUnitSyntax()
                {
                    CSharpMember = classDeclaration,
                    members = members,
                    Modifier = Modifier
                };
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
                .AddModifiers(SyntaxFactory.Token(Modifier))
                .AddBaseListTypes(SyntaxFactory.SimpleBaseType(SyntaxFactory.ParseTypeName("Codeunit")))
                .AddAttributeLists(SyntaxFactory.AttributeList().AddAttributes(SyntaxFactory.Attribute(SyntaxFactory.ParseName("ID"), 
                    SyntaxFactory.AttributeArgumentList()
                    .AddArguments(
                        SyntaxFactory.AttributeArgument(SyntaxFactory.ParseExpression(ID.ToString()))))))
                .AddMembers(tmp.ToArray())
                .NormalizeWhitespace();
        }
        
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
