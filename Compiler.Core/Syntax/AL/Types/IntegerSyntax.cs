using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.Core.Syntax.AL.Types
{
    public class IntegerSyntax : AlSourceTypeSyntax<PredefinedTypeSyntax>
    {
        public override bool TryParse(TypeSyntax typeSyntax, Func<TypeSyntax, SyntaxType> analyser, out SyntaxType syntaxType)
        {
            syntaxType = null;
            
            if(typeSyntax is PredefinedTypeSyntax predefinedType)
            {
                if (predefinedType.Keyword.ValueText != "int" && predefinedType.Keyword.ValueText != "Int32")
                    return false;

                syntaxType = new IntegerSyntax
                {
                    CSharpType = predefinedType,
                    Name = nameof(IntegerSyntax),
                    Keyword = predefinedType.Keyword.ValueText
                };

                return true;
            }

            return false;
        }

        public override string GetInitializer(EqualsValueClauseSyntax valueClauseSyntax) => valueClauseSyntax.Value.ToString();

        public override string ParseInitializer(string value) => value;

        internal override void ParseCSharp()
        {
            CSharpType = SyntaxFactory.PredefinedType(SyntaxFactory.ParseToken(Name));
        }
    }
}
