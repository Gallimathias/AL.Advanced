using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.Core.Syntax.AL.Statements
{
    public class ExitStatementSyntax : AlSourceStatementSyntax<ReturnStatementSyntax>
    {
        public string ReturnVariable { get; set; }

        public override bool TryParse(StatementSyntax statementSyntax,
            Func<StatementSyntax, SyntaxStatement> analyser, out SyntaxStatement syntaxStatement)
        {
            syntaxStatement = null;

            if (statementSyntax is ReturnStatementSyntax returnStatement)
            {
                var bodyClass = (ClassDeclarationSyntax)returnStatement.Parent.Parent.Parent;

                var retValue = (FieldDeclarationSyntax)bodyClass.Members.FirstOrDefault(m =>
                                 {
                                     if (m is FieldDeclarationSyntax field)
                                         return field.AttributeLists.FirstOrDefault(
                                             al => al.Attributes.FirstOrDefault(
                                                 a => a.Name.ToString() == "ReturnValue") != null) != null;

                                     return false;
                                 });

                syntaxStatement = new ExitStatementSyntax
                {
                    CSharpStatement = returnStatement,
                    Name = nameof(ExitStatementSyntax),
                    ReturnVariable = retValue.Declaration.Variables[0].Identifier.ToString()
                };

                return true;
            }

            return false;
        }

        internal override void ParseCSharp()
        {
            throw new NotImplementedException();
        }
    }
}
