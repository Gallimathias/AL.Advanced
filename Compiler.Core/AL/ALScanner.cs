using Compiler.Core.AL;
using Microsoft.CodeAnalysis.CSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.Core.AL
{
    public class ALScanner : IScanner
    {
        private CSharpSyntaxTree syntaxTree;

        public ALScanner(string source)
        {
            syntaxTree = (CSharpSyntaxTree)CSharpSyntaxTree.ParseText(source);
        }
        public ALScanner(Stream stream)
        {
            using (var reader = new StreamReader(stream))
            {
                syntaxTree = (CSharpSyntaxTree)CSharpSyntaxTree.ParseText(reader.ReadToEnd());
            }
        }

        public CSharpSyntaxTree Result => syntaxTree;

        public DefinitionFormat SourceDefinition => DefinitionFormat.AL;
    }
}
