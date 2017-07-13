using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;

namespace Compiler.Core
{
    internal class ScannerResult
    {
        public ScannerResult()
        {
            Parent = null;
        }

        public string Name { get; set; }
        public DefinitionFormat SourceDefinition { get; internal set; }
        public SyntaxKind RawType { get; internal set; }
        public BaseTypeSyntax SourceType { get; internal set; }
        public ScannerResult Parent { get; set; }
        public MemberDeclarationSyntax Source { get; internal set; }
    }
}