using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.Core.Syntax.ALAdvanced.Types
{
    [SyntaxSource(SyntaxSource.ALAdvanced)]
    public abstract class AlAdvancedTypeSyntax<TCSharpType> : SyntaxType where TCSharpType : TypeSyntax
    {
        public TCSharpType CSharpType { get; protected set; }

        internal override TypeSyntax GetCSharpSyntax() => CSharpType;

        internal override void Normalize() => CSharpType.NormalizeWhitespace();
    }
}
