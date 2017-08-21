using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.CSharp;

namespace Compiler.Core.Syntax.AL.Members
{
    public class CodeUnitSyntax : ObjectSyntax
    {
        public CodeUnitSyntax() : base()
        {
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
                    AddMember(analyser(member));

                ExtractNameAndId(classDeclaration);

                Modifier = classDeclaration.Modifiers.First().Kind();

                memberSyntax = new CodeUnitSyntax
                {
                    CSharpMember = classDeclaration,
                    Name = Name,
                    ID = ID,
                    members = members,
                    Modifier = Modifier

                };

                return true;
            }

            return false;

        }

        public override string ToString() => $"Codeunit[{ID}] {Name}";
        
        internal override void ParseCSharp()
        {
            var tmp = new List<MemberDeclarationSyntax>();

            foreach (var member in members)
            {
                member.ParseCSharp();
                tmp.Add(member.GetCSharpSyntax());
            }

            CSharpMember = SyntaxFactory.ClassDeclaration($"Codeunit{ID}")
                .AddModifiers(SyntaxFactory.Token(Modifier))
                .AddModifiers(SyntaxFactory.Token(SyntaxKind.SealedKeyword))
                .AddBaseListTypes(SyntaxFactory.SimpleBaseType(SyntaxFactory.ParseTypeName("NavCodeunit")))
                .AddAttributeLists(SyntaxFactory.AttributeList().AddAttributes(SyntaxFactory.Attribute(SyntaxFactory.ParseName("NavCodeunitOptions"))))
                .AddMembers(tmp.ToArray())
                .NormalizeWhitespace();
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

        private void ExtractNameAndId(ClassDeclarationSyntax classDeclaration)
        {
            ID = int.Parse(((string)classDeclaration.Identifier.Value).Replace("Codeunit", ""));

            var prop = (PropertyDeclarationSyntax)classDeclaration.Members.Where(m => m.Kind() == SyntaxKind.PropertyDeclaration)
                .FirstOrDefault(p => (string)((PropertyDeclarationSyntax)p).Identifier.Value == "ObjectName");

            var returnStatement = (ReturnStatementSyntax)prop.AccessorList.Accessors
                .First(a => a.Kind() == SyntaxKind.GetAccessorDeclaration)
                .Body.Statements
                .First(s => s.Kind() == SyntaxKind.ReturnStatement);

            Name = (string)((ParenthesizedExpressionSyntax)returnStatement.Expression)
                .Expression.GetFirstToken()
                .Value;
        }

    }
}
