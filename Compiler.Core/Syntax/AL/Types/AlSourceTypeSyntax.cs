using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.Core.Syntax.AL.Types
{
    [SyntaxSource(SyntaxSource.ALSource)]
    public abstract class AlSourceTypeSyntax<TCSharpType> : SyntaxType where TCSharpType : TypeSyntax
    {
        public TCSharpType CSharpType { get; protected set; }

        internal override TypeSyntax GetCSharpSyntax() => CSharpType;

        internal override void Normalize() => CSharpType.NormalizeWhitespace();

    }
}
