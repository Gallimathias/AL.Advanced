using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ALStatements = Compiler.Core.Syntax.AL.Statements;
using AlMembers = Compiler.Core.Syntax.AL.Members;
using AdvancedStatements = Compiler.Core.Syntax.ALAdvanced.Statements;

namespace Compiler.Core.Translators.Maps
{
    public class AlToAdvancedStatementMap : Map
    {

        public AlToAdvancedStatementMap(AlToAdvanced translator) : base(translator)
        {
        }

        public override object Invoke(string statmentname, object statement) => GetType().GetMethod(statmentname).Invoke(this, new[] { statement });

        public AdvancedStatements.LocalDeclarationSyntax LocalDeclarationSyntax(ALStatements.LocalDeclarationSyntax declarationSyntax)
        {
            var declatration = Copy<AdvancedStatements.LocalDeclarationSyntax>(declarationSyntax);

            declatration.SetType(((AlToAdvanced)Translator).TranslateType(declarationSyntax.Type));

            return declatration;
        }

        public AdvancedStatements.ExpressionSyntax ExpressionSyntax(ALStatements.ExpressionSyntax expressionSyntax) => Copy<AdvancedStatements.ExpressionSyntax>(expressionSyntax);

        public AdvancedStatements.ReturnSyntax ExitStatementSyntax(ALStatements.ExitStatementSyntax exitStatement)
        {
            var cop = Copy<AdvancedStatements.ReturnSyntax>(exitStatement);

            var body = (AlMembers.MethodBodySyntax)exitStatement.Parent;

            foreach (var statement in body.Statements)
            {
                if(statement is ALStatements.ExpressionSyntax expression) { 
                    if (expression.Kind != "SimpleAssignmentExpression")
                        continue;

                    if (!expression.Expression.Contains("="))
                        continue;

                    if (!expression.Expression.StartsWith(exitStatement.ReturnVariable))
                        continue;

                    var spl = expression.Expression.Split('=');

                    cop.Expression = spl[spl.Length - 1];
                    break;
                }

            }

            return cop;
        }
    }
}
