using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Compiler.Core.Syntax.AL.Statements
{
    [SyntaxSource(SyntaxSource.ALSource)]
    public abstract class AlSourceStatementSyntax<TCSharpStatement> : SyntaxStatement where TCSharpStatement : StatementSyntax
    {
        public TCSharpStatement CSharpStatement { get; protected set; }
        public abstract override bool TryParse(StatementSyntax statementSyntax, Func<StatementSyntax, SyntaxStatement> analyser, out SyntaxStatement syntaxStatement);

        internal override StatementSyntax GetCSharpSyntax() => CSharpStatement; 
    }
}
