using AL.Advanced.Core.Definition;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Al.Advanced.Definition.CSharp
{
    internal class ObjectDeclaration : ALObject<MemberDeclarationSyntax>
    {
        public ObjectDeclaration(ObjectType type)
        {
            ObjectType = type;
        }

        public override bool Check(MemberDeclarationSyntax root)
        {
            if (root is ClassDeclarationSyntax classDeclaration)
            {
                var typeName = classDeclaration.BaseList.Types.FirstOrDefault()?.ToString();

                if (string.IsNullOrWhiteSpace(typeName))
                    return false;

                return typeName.ToLower() == "codeunit";
            }

            return false;
        }

        public override void Parse(MemberDeclarationSyntax root)
        {
            if (!TryParse(root))
                throw new Exception("Could not Parse");
        }

        public override string ToText()
        {
            var builder = new StringBuilder();
            builder.AppendLine($"public class {Name} : Codeunit {{");
            builder.AppendLine($"public {Name}() : base({ID}) {{}} }}");

            return CSharpSyntaxTree
                    .ParseText(builder.ToString())
                    .GetCompilationUnitRoot()
                    .NormalizeWhitespace()
                    .ToFullString();
        }

        public override bool TryParse(MemberDeclarationSyntax root)
        {
            if (!Check(root))
                return false;

            var classDeclaration = (ClassDeclarationSyntax)root;
            var initializer = classDeclaration
                 .Members
                 .Select(m => m is ConstructorDeclarationSyntax ctor ? ctor : null)
                 .Where(c => c != null)
                 .FirstOrDefault(c => c.Initializer.Kind() == SyntaxKind.BaseConstructorInitializer)
                 .Initializer
                 .ToString();

            var start = initializer.IndexOf('(') + 1;
            ID = int.Parse(initializer.Substring(start, initializer.IndexOf(')', start) - start));
            Name = (string)classDeclaration.Identifier.Value;
            return true;
        }
    }
}
