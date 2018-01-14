using AL.Advanced.Core.Definition;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Formatting;

namespace AL.Advanced.Definition.CSharpALFormatting.Object
{
    internal class ObjectDeclaration : ALObject<MemberDeclarationSyntax>
    {
        public ObjectDeclaration()
        {
        }
        public ObjectDeclaration(ObjectType type)
        {
            ObjectType = type;
        }

        public override string ToText()
        {
            var builder = new StringBuilder();
            builder.AppendLine("[NavCodeunitOptions()]");
            builder.AppendLine($"public sealed class Codeunit{ID} : NavCodeunit {{");
            builder.AppendLine($"public Codeunit{ID}(ITreeObject parent) : base(parent, {ID}){{}}");
            builder.AppendLine($"public override string ObjectName => @\"{Name}\";");
            builder.AppendLine();
            builder.AppendLine(
                $"public static Codeunit{ID} __Construct(ITreeObject parent) => new Codeunit{ID}(parent); }}");
            
            return CSharpSyntaxTree
                    .ParseText(builder.ToString())
                    .GetCompilationUnitRoot()
                    .NormalizeWhitespace()
                    .ToFullString();
        }

        public override bool Check(MemberDeclarationSyntax root)
        {
            if (root is ClassDeclarationSyntax classDeclaration)
            {
                var typeName = classDeclaration.BaseList.Types.FirstOrDefault()?.ToString();

                if (string.IsNullOrWhiteSpace(typeName))
                    return false;

                return typeName.ToLower() == "navcodeunit";
            }

            return false;
        }

        public override void Parse(MemberDeclarationSyntax root)
        {
            if (!TryParse(root))
                throw new Exception("Could not Parse");
        }

        public override bool TryParse(MemberDeclarationSyntax root)
        {
            if (!Check(root))
                return false;

            var classDeclaration = (ClassDeclarationSyntax)root;
            ID = int.Parse(((string)classDeclaration.Identifier.Value).Substring(8));

            var objNameField = classDeclaration
                                .Members
                                .Select(m => m is PropertyDeclarationSyntax property ? property : null)
                                .Where(m => m != null)
                                .First(m => "ObjectName" == (string)m.Identifier.Value);

            Name = objNameField
                     .AccessorList
                     .Accessors
                     .First(a => a.Kind() == SyntaxKind.GetAccessorDeclaration)
                     .ToFullString();

            var start = Name.IndexOf('"') + 1;

            Name = Name.Substring(start, Name.IndexOf('"', start) - start);

            return true;
        }
        
    }
}
