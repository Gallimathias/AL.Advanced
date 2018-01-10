using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.Core.Syntax
{
    public delegate bool SyntaxTypeParseDelegate(
        TypeSyntax typeSyntax,
        Func<TypeSyntax, SyntaxType> analyser,
        out SyntaxType syntaxType);

    public abstract class SyntaxType
    {
        public string Name { get; set; }
        public string Keyword { get; set; }

        public abstract bool TryParse(TypeSyntax typeSyntax, Func<TypeSyntax, SyntaxType> analyser, out SyntaxType syntaxType);

        public abstract string GetInitializer(EqualsValueClauseSyntax valueClauseSyntax);

        public abstract string ParseInitializer(string value);

        internal abstract void ParseCSharp();

        internal abstract TypeSyntax GetCSharpSyntax();

        internal abstract void Normalize();
        
    }
}
