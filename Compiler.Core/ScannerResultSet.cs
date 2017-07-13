using System;
using System.Collections.Generic;

namespace Compiler.Core
{
    public class ScannerResultSet
    {
        private DefinitionFormat sourceDefinition;
        private List<ScannerResult> scannerResults;

        public ScannerResultSet(DefinitionFormat sourceDefinition)
        {
            this.sourceDefinition = sourceDefinition;
            scannerResults = new List<ScannerResult>();
        }

        internal void Add(ScannerResult alResult)
        {
            scannerResults.Add(alResult);
        }
    }
}