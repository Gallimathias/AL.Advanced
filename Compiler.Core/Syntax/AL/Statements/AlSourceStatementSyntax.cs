using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis;

namespace Compiler.Core.Syntax.AL.Statements
{
    [SyntaxSource(SyntaxSource.ALSource)]
    public abstract class AlSourceStatementSyntax<TCSharpStatement> : SyntaxStatement where TCSharpStatement : StatementSyntax
    {
        public TCSharpStatement CSharpStatement { get; protected set; }
        internal override StatementSyntax GetCSharpSyntax() => CSharpStatement;

        internal override void Normalize() => CSharpStatement.NormalizeWhitespace();
    }
}
