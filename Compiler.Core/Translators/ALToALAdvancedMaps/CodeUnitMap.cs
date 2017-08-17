using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AL = Compiler.Core.Syntax.AL;
using ALAdvanced = Compiler.Core.Syntax.ALAdvanced;

namespace Compiler.Core.Translators.ALToALAdvancedMaps
{
    public class CodeUnitMap
    {
        private ALAdvanced.CodeUnitSyntax advancedCodeUnit;

        public CodeUnitMap(AL.CodeUnitSyntax codeUnit)
        {
            
        }

        public bool Translate()
        {
            advancedCodeUnit = new ALAdvanced.CodeUnitSyntax();
            advancedCodeUnit.ParseCSharp();
            


            throw new NotImplementedException();
        }
    }
}
