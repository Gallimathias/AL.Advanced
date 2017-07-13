using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Editing;
using Microsoft.CodeAnalysis.Text;

namespace Compiler.Core.ALAdvanced
{
    internal class AdvancedBuilder : IDefinitionBuilder
    {
        public MemberDeclarationSyntax CurrentSourceNode { get; private set; }
        public MemberDeclarationSyntax CurrentNode { get; private set; }

        private Generator generator;
        private CompilationUnitSyntax unit;
        private CSharpSyntaxTree sourceTree;
        private CompilationUnitSyntax sourceUnit;



        public AdvancedBuilder(Generator generator, CSharpSyntaxTree tree)
        {
            this.generator = generator;
            sourceTree = tree;

            sourceUnit = tree.GetCompilationUnitRoot();
            unit = SyntaxFactory.CompilationUnit();
            unit = unit.AddUsings(SyntaxFactory.UsingDirective(SyntaxFactory.ParseName("System")));
            unit = unit.AddUsings(SyntaxFactory.UsingDirective(SyntaxFactory.ParseName("AL")));
            CurrentSourceNode = (NamespaceDeclarationSyntax)sourceUnit.Members.FirstOrDefault(
                m => m.Kind() == SyntaxKind.NamespaceDeclaration);

            CurrentNode = generator.GenerateNamespace("AL.Extentibility");
        }

        public void Create()
        {
            var codunit = new CodeunitBuilder(generator, sourceTree);
            codunit.Create();
            CurrentSourceNode = codunit.CurrentSourceNode;
            CurrentNode = codunit.CurrentNode;
        }
    }
}
