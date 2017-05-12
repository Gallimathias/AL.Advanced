using Compiler.Core.AL;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
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
        public ScannerResultSet Result { get; private set; }
        public DefinitionFormat SourceDefinition => DefinitionFormat.AL;

        private CSharpSyntaxTree syntaxTree;
        private CompilationUnitSyntax root;

        public ALScanner(string source)
        {
            ALScannerInit(source);
        }
        public ALScanner(Stream stream)
        {
            using (var reader = new StreamReader(stream))
            {
                ALScannerInit(reader.ReadToEnd());
            }
        }

        public ALScanner()
        {
            Result = new ScannerResultSet(SourceDefinition);
        }

        public void Scan()
        {
            var classes = ((NamespaceDeclarationSyntax)root.Members.FirstOrDefault(
                m => m.Kind() == SyntaxKind.NamespaceDeclaration))?.Members.Where(
                    c => c.Kind() == SyntaxKind.ClassDeclaration);

            foreach (ClassDeclarationSyntax obj in classes)
            {
                var alResult = new ScannerResult()
                {
                    Name = obj.Identifier.ValueText,
                    SourceDefinition = SourceDefinition,
                    RawType = SyntaxKind.ClassDeclaration
                };

                var type = obj.BaseList.Types.FirstOrDefault(b => b.GetType().IsAbstract);
                alResult.SourceType = type;

                Result.Add(alResult);

            }
        }

        private void ALScannerInit(string source)
        {
            syntaxTree = (CSharpSyntaxTree)CSharpSyntaxTree.ParseText(source);
            root = syntaxTree.GetCompilationUnitRoot();
        }




    }
}
