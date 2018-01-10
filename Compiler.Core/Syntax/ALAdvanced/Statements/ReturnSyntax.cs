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
    public class ReturnSyntax : AlAdvancedStatementSyntax<ReturnStatementSyntax>
    {
        public string Expression { get; set; }

        public override bool TryParse(StatementSyntax statementSyntax, 
            Func<StatementSyntax, SyntaxStatement> analyser, out SyntaxStatement syntaxStatement)
        {
            syntaxStatement = null;

            if(statementSyntax is ReturnStatementSyntax returnStatement)
            {
                syntaxStatement = new ReturnSyntax
                {
                    CSharpStatement = returnStatement,
                    Expression = returnStatement.Expression.ToString().TrimEnd(';'),
                    Name = nameof(ReturnSyntax)
                };

                return true;
            }

            return false;
        }

        internal override void ParseCSharp()
        {
            CSharpStatement = SyntaxFactory.ReturnStatement(
                SyntaxFactory.ParseExpression(Expression))
                .NormalizeWhitespace();
        }
    }
}
