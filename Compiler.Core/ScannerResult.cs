using Microsoft.CodeAnalysis.CSharp;
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
        public Type SourceType { get; internal set; }
        public ScannerResult Parent { get; set; }
    }
}