using Microsoft.CodeAnalysis.CSharp;

namespace Compiler.Core
{
    public interface IScanner
    {
        ScannerResultSet Result { get; }
        DefinitionFormat SourceDefinition { get; }
    }
}