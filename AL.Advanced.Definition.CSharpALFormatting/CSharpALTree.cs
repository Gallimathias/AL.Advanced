using AL.Advanced.Core;
using AL.Advanced.Core.Definition;
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
            if (TryParse(text, out CSharpALTree tree))
                return tree;

            throw new Exception("Could not parse text");
        }
        public static bool TryParse(string text, out CSharpALTree tree)
        {
            var list = CSharpSyntaxTree.ParseText(text).GetCompilationUnitRoot().Members;
            var scanner = new CSharpALScanner();

            foreach (NamespaceDeclarationSyntax namespaceDeclarationSyntax in list)
            {
                foreach (ClassDeclarationSyntax classDeclaration in namespaceDeclarationSyntax.Members)
                {
                    scanner.TryScan(classDeclaration, out Member<MemberDeclarationSyntax>  obj);
                }
            }

            throw new NotImplementedException();
        }
    }
}
