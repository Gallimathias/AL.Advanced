using Compiler.Core.ALAdvanced;
using Microsoft.CodeAnalysis.CSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.Core
{
    public class Parser
    {
        private IScanner scanner;
        private Generator generator;

        public Parser(IScanner scanner)
        {
            this.scanner = scanner;
            generator = new Generator();
        }

        public void Parse()
        {
            DefinitionFormat sourceDefinition = scanner.SourceDefinition;
            CSharpSyntaxTree tree = scanner.Result;

            switch (sourceDefinition)
            {
                case DefinitionFormat.AL:
                    toAdvanced(tree);
                    break;
                case DefinitionFormat.ALAdvanced:
                    toAl(tree);
                    break;
                case DefinitionFormat.ALClassic:
                case DefinitionFormat.ALExtended:
                    new NotImplementedException("Is not yet implemented in this version");
                    break;
                default:
                    new NotSupportedException($"Not supported definition. Sourcedefinition: {sourceDefinition}");
                    break;
            }
        }

        private void toAdvanced(CSharpSyntaxTree tree)
        {
            var builder = new AdvancedBuilder(generator, tree);
            builder.Create();
        }

        private void toAl(CSharpSyntaxTree tree)
        {

        }
    }
}
