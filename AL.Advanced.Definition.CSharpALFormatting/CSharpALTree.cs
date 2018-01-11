using AL.Advanced.Core;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Text;

namespace AL.Advanced.Definition.CSharpALFormatting
{
    public class CSharpALTree : Tree<CSharpALTree>
    {
        public static CSharpALTree Parse(string text)
        {
            throw new NotImplementedException();
        }
        public static bool TryParse(string text, out CSharpALTree tree)
        {
            var list = CSharpSyntaxTree.ParseText(text).GetCompilationUnitRoot().Members;

            foreach (NamespaceDeclarationSyntax namespaceDeclarationSyntax in list)
            {
            }

            throw new NotImplementedException();
        }
    }
}
