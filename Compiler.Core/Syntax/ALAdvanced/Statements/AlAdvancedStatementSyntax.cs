using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.Core.Syntax.ALAdvanced.Statements
{
    public abstract class AlAdvancedStatementSyntax<TCSharpStatement> : SyntaxStatement where TCSharpStatement : StatementSyntax
    {
        public TCSharpStatement CSharpStatement { get; protected set; }
        public abstract override bool TryParse(StatementSyntax statementSyntax, Func<StatementSyntax, SyntaxStatement> analyser, out SyntaxStatement syntaxStatement);

        internal override StatementSyntax GetCSharpSyntax() => CSharpStatement;
    }
}
