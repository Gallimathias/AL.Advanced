using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.Core.Syntax
{
    public delegate bool SyntaxStatementParseDelegate(
        StatementSyntax statementSyntax,
        Func<StatementSyntax, SyntaxStatement> analyser,
        out SyntaxStatement syntaxStatement);

    public abstract class SyntaxStatement
    {
        public SyntaxMember Parent { get; set; }

        public string Name { get; set; }

        public abstract bool TryParse(StatementSyntax statementSyntax, Func<StatementSyntax, SyntaxStatement> analyser, out SyntaxStatement syntaxStatement);

        internal abstract void ParseCSharp();

        internal abstract StatementSyntax GetCSharpSyntax();

        internal abstract void Normalize();
    }
}
