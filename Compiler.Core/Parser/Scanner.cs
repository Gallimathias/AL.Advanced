using Compiler.Core.Syntax;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.Core.Parser
{
    public class Scanner
    {
        private MemberDeclarationSyntax[] tokens;
        private Dictionary<int, SyntaxParseDelegate> syntaxDictionary;

        public Scanner(params MemberDeclarationSyntax[] tokens)
        {
            this.tokens = tokens;
        }

        public void Scan()
        {
            foreach (var token in tokens)
            {
                Analyse(token);
            }
        }

        private MemberSyntax Analyse(MemberDeclarationSyntax memberDeclaration)
        {
            foreach (var syntax in syntaxDictionary)
            {
                if (syntax.Value(memberDeclaration, Analyse, out MemberSyntax memberSyntax))
                    return memberSyntax;

                continue;
            }

            throw new Exception("No valid expression");
        }
    }
}
