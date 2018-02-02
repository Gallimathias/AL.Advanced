using AL.Advanced.Core;
using AL.Advanced.Core.Definition;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Text;

namespace Al.Advanced.Definition.CSharp
{
    public class CSharpTree : Tree
    {
        public CSharpTree(UnitRoot unitRoot) : base(unitRoot)
        {
        }

        public static CSharpTree Parse(string text)
        {
            if (TryParse(text, out CSharpTree tree))
                return tree;

            throw new Exception("Could not parse text");
        }
        public static bool TryParse(string text, out CSharpTree tree)
        {
            var list = CSharpSyntaxTree.ParseText(text).GetCompilationUnitRoot().Members;
            var scanner = new CSharpScanner();
            var tmpList = new List<Member>();

            foreach (NamespaceDeclarationSyntax namespaceDeclarationSyntax in list)
            {
                foreach (ClassDeclarationSyntax classDeclaration in namespaceDeclarationSyntax.Members)
                {
                    if (scanner.TryScan(classDeclaration, out Member obj))
                    {
                        tmpList.Add(obj);
                    }
                    else
                    {
                        tree = null;
                        return false;
                    }
                }
            }

            tree = new CSharpTree(new UnitRoot(tmpList));
            return true;
        }

        public override void CopyRoot(UnitRoot unitRoot)
        {
            var scanner = new CSharpScanner();

            foreach (var member in unitRoot.Members)
            {
                if (scanner.TryGetCopy(member, out Member newMember))
                    UnitRoot.Members.Add(newMember);
                else
                    throw new Exception("Can not copy Member");
            }
            
        }
    }
}
