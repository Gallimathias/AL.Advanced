using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.Core.Syntax.ALAdvanced.Statements
{
    public class ExpressionSyntax : AlAdvancedStatementSyntax<ExpressionStatementSyntax>
    {
        public string Expression { get; set; }

        public override bool TryParse(StatementSyntax statementSyntax,
            Func<StatementSyntax, SyntaxStatement> analyser, out SyntaxStatement syntaxStatement)
        {
            syntaxStatement = null;

            if (statementSyntax is ExpressionStatementSyntax expression)
            {
                syntaxStatement = new ExpressionSyntax
                {
                    CSharpStatement = expression,
                    Name = nameof(ExpressionSyntax),
                    Expression = expression.ToString()
                };

                return true;
            }

            return false;
        }

        internal override void ParseCSharp()
        {
            CSharpStatement = SyntaxFactory.ExpressionStatement(SyntaxFactory.ParseExpression(Expression));
        }
    }
}
