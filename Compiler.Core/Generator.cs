using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Editing;
using Microsoft.CodeAnalysis.Text;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.Core
{
    internal class Generator
    {
        public NamespaceDeclarationSyntax RootNamespace { get; private set; }

        public Generator()
        {
            
        }

        public NamespaceDeclarationSyntax GenerateNamespace(string nameIdentifier)
        {
            var name = SyntaxFactory.ParseName(nameIdentifier);
            RootNamespace = SyntaxFactory.NamespaceDeclaration(name);
            return RootNamespace;
        }

        public void Write(string path)
        {
            var editor = new SyntaxEditor(RootNamespace, new AdhocWorkspace());
            RootNamespace.NormalizeWhitespace();
            editor.AddMember(editor.OriginalRoot, RootNamespace);
            var tree = CSharpSyntaxTree.Create((CSharpSyntaxNode)editor.GetChangedRoot(), path: path);

            using (var writer = new StreamWriter(File.OpenWrite(tree.FilePath)))
            {
                tree.GetText().Write(writer);
            } 
        }

        public Document ToDocument(Project project, string name)
        {
            var editor = new SyntaxEditor(RootNamespace, new AdhocWorkspace());
            RootNamespace.NormalizeWhitespace();
            editor.AddMember(editor.OriginalRoot, RootNamespace);
            var tree = CSharpSyntaxTree.Create((CSharpSyntaxNode)editor.GetChangedRoot());
            return project.AddDocument(name, tree.GetText());            
        }

        public override string ToString()
        {
            var editor = new SyntaxEditor(RootNamespace, new AdhocWorkspace());
            RootNamespace.NormalizeWhitespace();
            editor.AddMember(editor.OriginalRoot, RootNamespace);
            return CSharpSyntaxTree.Create((CSharpSyntaxNode)editor.GetChangedRoot()).GetText().ToString();
        }

    }
}
