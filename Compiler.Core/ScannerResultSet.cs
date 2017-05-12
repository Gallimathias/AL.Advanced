using System;

namespace Compiler.Core
{
    public class ScannerResultSet
    {
        private DefinitionFormat sourceDefinition;

        public ScannerResultSet(DefinitionFormat sourceDefinition)
        {
            this.sourceDefinition = sourceDefinition;
        }

        internal void Add(ScannerResult alResult)
        {
            throw new NotImplementedException();
        }
    }
}