using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.Core.Tokenizer
{
    public class TokenScanner
    {
        private CompilationUnitSyntax root;
        public TokenScanner(string source)
        {
            root = CSharpSyntaxTree.ParseText(source).GetCompilationUnitRoot();
        }

        public MemberDeclarationSyntax[] Scan()
        {
            var space = (NamespaceDeclarationSyntax)root.Members.FirstOrDefault(m => m.Kind() == SyntaxKind.NamespaceDeclaration);
            return space.Members.ToArray();
        }
    }
}
