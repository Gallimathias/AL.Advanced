using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.Core.Syntax.AL.Statements
{
    public class ExpressionSyntax : AlSourceStatementSyntax<ExpressionStatementSyntax>
    {
        public override bool TryParse(StatementSyntax statementSyntax, 
            Func<StatementSyntax, SyntaxStatement> analyser, out SyntaxStatement syntaxStatement)
        {
            syntaxStatement = null;

            if(statementSyntax is ExpressionStatementSyntax expression)
            {
                syntaxStatement = new ExpressionSyntax {
                    CSharpStatement = expression,
                    Name = nameof(ExpressionSyntax)
                };

                return true;
            }

            return false;
        }

        internal override void Normalize()
        {
            throw new NotImplementedException();
        }

        internal override void ParseCSharp()
        {
            throw new NotImplementedException();
        }
    }
}
