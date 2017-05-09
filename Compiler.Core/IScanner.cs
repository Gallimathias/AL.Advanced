using Microsoft.CodeAnalysis.CSharp;

namespace Compiler.Core
{
    public interface IScanner
    {
        CSharpSyntaxTree Result { get; }
        DefinitionFormat SourceDefinition { get; }
    }
}