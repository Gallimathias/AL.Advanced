using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.Core
{
    public class Scanner
    {
        private CompilationUnitSyntax rootUnit;

        public Scanner(string source)
        {
            //rootUnit = (CompilationUnitSyntax)CSharpSyntaxTree.ParseText(source).GetRoot();
        }

        public void Scan()
        {
            
        }

        private void ScanNamespaces()
        {
            foreach (var namespaceDelcaration in rootUnit.Members)
            {
                var tmpNamespace = (NamespaceDeclarationSyntax)namespaceDelcaration;
                
            }

        }

        private void ScanClasses()
        {

        }

        private void ScanMethods()
        {

        }

        private void ScanProperties()
        {

        }
    }
}
