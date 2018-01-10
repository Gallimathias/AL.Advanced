using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.Core.Syntax.ALAdvanced.Statements
{
    [SyntaxSource(SyntaxSource.ALAdvanced)]
    public abstract class AlAdvancedStatementSyntax<TCSharpStatement> : SyntaxStatement where TCSharpStatement : StatementSyntax
    {
        public TCSharpStatement CSharpStatement { get; protected set; }

        internal override StatementSyntax GetCSharpSyntax() => CSharpStatement;

        internal override void Normalize() => CSharpStatement.NormalizeWhitespace();
    }
}
