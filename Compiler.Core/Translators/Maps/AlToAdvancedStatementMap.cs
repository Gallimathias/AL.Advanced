using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ALStatements = Compiler.Core.Syntax.AL.Statements;
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


    }
}
