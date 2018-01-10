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
        public CompilationUnitSyntax Root { get; private set; }

        public Generator()
        {
            Root = SyntaxFactory.CompilationUnit();
            
        }
            
        
        public void AddMembers(params MemberDeclarationSyntax[] members)
        {
            Root = Root.AddMembers(members);
        }

        public void AddUsings(params UsingDirectiveSyntax[] usings)
        {
            Root = Root.AddUsings(usings);
        }

        public NamespaceDeclarationSyntax GenerateNamespace(string nameIdentifier) =>
            GenerateNamespace(SyntaxFactory.ParseName(nameIdentifier));
        public NamespaceDeclarationSyntax GenerateNamespace(NameSyntax name)
        {
            var rootNamespace = SyntaxFactory.NamespaceDeclaration(name);
            return rootNamespace;
        }

        public void Write(string path)
        {
            var editor = new SyntaxEditor(Root, new AdhocWorkspace());
            Root.NormalizeWhitespace();
            editor.AddMember(editor.OriginalRoot, Root);
            var tree = CSharpSyntaxTree.Create((CSharpSyntaxNode)editor.GetChangedRoot(), path: path);

            using (var writer = new StreamWriter(File.OpenWrite(tree.FilePath)))
            {
                tree.GetText().Write(writer);
            }
        }

        public Document ToDocument(Project project, string name)
        {
            var editor = new SyntaxEditor(Root, new AdhocWorkspace());
            Root.NormalizeWhitespace();
            editor.AddMember(editor.OriginalRoot, Root);
            var tree = CSharpSyntaxTree.Create((CSharpSyntaxNode)editor.GetChangedRoot());
            return project.AddDocument(name, tree.GetText());
        }

        public override string ToString()
        {
            var editor = new SyntaxEditor(Root, new AdhocWorkspace());
            Root.NormalizeWhitespace();
            editor.AddMember(editor.OriginalRoot, Root);
            return CSharpSyntaxTree.Create((CSharpSyntaxNode)editor.GetChangedRoot()).GetText().ToString();
        }

    }
}
