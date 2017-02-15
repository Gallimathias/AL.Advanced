using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.Core.ALAdvanced
{
    internal class CodeunitBuilder : ISubBuilder
    {
        public MemberDeclarationSyntax CurrentSourceNode { get; private set; }
        public MemberDeclarationSyntax CurrentNode { get; private set; }

        private Generator generator;
        private CompilationUnitSyntax unit;
        private CSharpSyntaxTree sourceTree;
        private CompilationUnitSyntax sourceUnit;



        public CodeunitBuilder(Generator generator, CSharpSyntaxTree tree)
        {
            this.generator = generator;
            sourceTree = tree;

            sourceUnit = tree.GetCompilationUnitRoot();
            unit = SyntaxFactory.CompilationUnit();
            unit = unit.AddUsings(SyntaxFactory.UsingDirective(SyntaxFactory.ParseName("System")));
            unit = unit.AddUsings(SyntaxFactory.UsingDirective(SyntaxFactory.ParseName("AL")));
            CurrentSourceNode = (NamespaceDeclarationSyntax)sourceUnit.Members.FirstOrDefault(
                m => m.Kind() == SyntaxKind.NamespaceDeclaration);

            CurrentNode = generator.GenerateNamespace("AL.Extentibility");
        }

        public void Create()
        {
            baseClass();
            buildMethods();
        }

        private void buildMethods()
        {
            var classDeclaration = (ClassDeclarationSyntax)CurrentSourceNode;
            var methods = classDeclaration.Members.Where(m =>
                                                       m.Kind() == SyntaxKind.MethodDeclaration &&
                                                       ((MethodDeclarationSyntax)m).Modifiers.Any(
                                                           mo => mo.Kind() == SyntaxKind.PrivateKeyword));
            foreach (MethodDeclarationSyntax method in methods)
            {
                var methodName = method.Identifier.ValueText;

                var scope = classDeclaration.Members.FirstOrDefault(
                                     m => m.Kind() == SyntaxKind.ClassDeclaration &&
                                        ((ClassDeclarationSyntax)m).Identifier.ValueText == $"{methodName}_Scope");
                var builder = new MethodBuilder(scope);
                ((ClassDeclarationSyntax)CurrentNode).AddMembers(builder.Create());
            }

        }

        private void baseClass()
        {
            CurrentSourceNode = ((NamespaceDeclarationSyntax)CurrentSourceNode).Members.FirstOrDefault(
                    m => m.Kind() == SyntaxKind.ClassDeclaration);
            var classDeclaration = (ClassDeclarationSyntax)CurrentSourceNode;
            var className = "";

            foreach (PropertyDeclarationSyntax prop in classDeclaration.Members.Where(
                m => m.Kind() == SyntaxKind.PropertyDeclaration))
            {
                if (prop.Identifier.Text != "ObjectName")
                    continue;

                var ret = (ReturnStatementSyntax)prop.AccessorList.Accessors[0].Body.Statements[0];

                var lit = (LiteralExpressionSyntax)((ParenthesizedExpressionSyntax)ret.Expression).Expression;
                className = lit.Token.ValueText;
                break;
            }

            var codeUnitClass = SyntaxFactory.ClassDeclaration(className);
            foreach (var type in classDeclaration.BaseList.Types)
            {
                if (((IdentifierNameSyntax)type.Type).Identifier.Text != "NavCodeunit")
                    continue;

                var baseType = SyntaxFactory.SimpleBaseType(SyntaxFactory.ParseTypeName("Codeunit"));
                codeUnitClass = codeUnitClass.AddBaseListTypes(baseType);
                break;
            }

            ConstructorDeclarationSyntax ctor = (ConstructorDeclarationSyntax)classDeclaration.Members.FirstOrDefault(
                m => m.Kind() == SyntaxKind.ConstructorDeclaration);

            var argument = ctor.Initializer.ArgumentList.Arguments[1];

            var id = (int)((LiteralExpressionSyntax)argument.Expression).Token.Value;

            var token = SyntaxFactory.Literal(id);

            var colon = SyntaxFactory.NameColon(SyntaxFactory.IdentifierName("id"));
            var exp = SyntaxFactory.LiteralExpression(SyntaxKind.NumericLiteralExpression, token);

            var attribute = SyntaxFactory.Attribute(SyntaxFactory.ParseName("CodeunitOptions"));
            attribute = attribute.AddArgumentListArguments(SyntaxFactory.AttributeArgument(null, colon, exp));
            var list = SyntaxFactory.AttributeList();
            list = list.AddAttributes(attribute);
            codeUnitClass = codeUnitClass.AddAttributeLists(list);
            var ctorList = SyntaxFactory.TokenList();
            ctorList.Add(SyntaxFactory.Token(SyntaxKind.PublicKeyword));
            ctor = SyntaxFactory.ConstructorDeclaration(
                SyntaxFactory.List<AttributeListSyntax>(),
                ctorList,
                SyntaxFactory.Identifier(className), SyntaxFactory.ParameterList(),
                SyntaxFactory.ConstructorInitializer(SyntaxKind.BaseConstructorInitializer),
                SyntaxFactory.Block());
            codeUnitClass = codeUnitClass.AddMembers(ctor);

            CurrentNode = ((NamespaceDeclarationSyntax)CurrentNode).AddMembers(codeUnitClass);

            CurrentNode = codeUnitClass;
        }
    }
}
