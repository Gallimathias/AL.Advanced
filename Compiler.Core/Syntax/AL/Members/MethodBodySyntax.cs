using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace Compiler.Core.Syntax.AL.Members
{

    public class MethodBodySyntax : ObjectSyntax
    {
        public List<SyntaxStatement> Statements { get; set; }

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

                var body =(MethodHeadSyntax)members.FirstOrDefault(m =>
                {
                    if(m is MethodHeadSyntax head)
                    {
                        if (head.Identifier == "OnRun")
                            return true;
                    }

                    return false;
                });

                var tmpList = new List<SyntaxStatement>();

                foreach (var statement in body.CSharpMember.Body.Statements)
                   tmpList.Add(AlParser.ParseStatement(statement));
                
                memberSyntax = new MethodBodySyntax
                {
                    CSharpMember = classDeclaration,
                    members = members,
                    Identifier = (string)classDeclaration.Identifier.Value,
                    Statements = tmpList

                };
                return true;
            }

            return false;
        }

        public override string ToString() => $"Method Body {Identifier}";

        internal override void ParseCSharp()
        {
            var tmp = new List<MemberDeclarationSyntax>();
            foreach (var member in members)
            {
                member.ParseCSharp();
                tmp.Add(member.GetCSharpSyntax());
            }

            var codeunit = (CodeUnitSyntax)Parent;
            CSharpMember = SyntaxFactory.ClassDeclaration(Identifier)
                .WithKeyword(SyntaxFactory.Token(SyntaxKind.PrivateKeyword))
                .AddBaseListTypes(SyntaxFactory.SimpleBaseType(SyntaxFactory.ParseTypeName($"NavMethodScope<{codeunit.Name}>")))
                .AddAttributeLists(SyntaxFactory.AttributeList().AddAttributes(SyntaxFactory.Attribute(SyntaxFactory.ParseName("SourceSpans"),
                    SyntaxFactory.AttributeArgumentList()
                    .AddArguments(
                        SyntaxFactory.AttributeArgument(SyntaxFactory.ParseExpression("0"))))))
                .AddMembers(tmp.ToArray())
                .NormalizeWhitespace();
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
