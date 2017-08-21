using Microsoft.CodeAnalysis.CSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Compiler.Core.Syntax.AL.Statements
{
    public class LocalDeclarationSyntax : AlSourceStatementSyntax<LocalDeclarationStatementSyntax>
    {

        public override bool TryParse(StatementSyntax statementSyntax,
            Func<StatementSyntax, SyntaxStatement> analyser, out SyntaxStatement syntaxStatement)
        {
            syntaxStatement = null;

            if (statementSyntax is LocalDeclarationStatementSyntax declaration)
            {
                syntaxStatement = new LocalDeclarationSyntax()
                {
                    CSharpStatement = declaration,
                    Name = nameof(LocalDeclarationSyntax)
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
