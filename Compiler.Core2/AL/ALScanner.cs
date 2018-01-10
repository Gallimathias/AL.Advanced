using Compiler.Core.AL;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

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
                    RawType = obj.Kind(),
                    Source = obj,
                    SourceType = obj.BaseList.Types.FirstOrDefault()
                };


                Result.Add(alResult);

                GetResultsFromClass(alResult);

            }
        }

        private void GetResultsFromClass(ScannerResult alResult)
        {
            var collection = ((ClassDeclarationSyntax)alResult.Source).Members.Where(
                                 m => m.Kind() != SyntaxKind.ClassDeclaration);

            foreach (var obj in collection)
            {
                Result.Add(new ScannerResult()
                {
                    //Name = obj.Identifier.ValueText,
                    SourceDefinition = SourceDefinition,
                    RawType = obj.Kind(),
                    Source = obj,
                    Parent = alResult
                });
                
            }
           
        }

        private void ALScannerInit(string source)
        {
            Result = new ScannerResultSet(SourceDefinition);
            syntaxTree = (CSharpSyntaxTree)CSharpSyntaxTree.ParseText(source);
            root = syntaxTree.GetCompilationUnitRoot();
        }




    }
}
