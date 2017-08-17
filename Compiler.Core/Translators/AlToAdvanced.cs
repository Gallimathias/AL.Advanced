using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Compiler.Core.Syntax;
using Compiler.Core.Parser;

namespace Compiler.Core.Translators
{
    public class AlToAdvanced : Translator
    {
        public AlToAdvanced(ALSyntaxTree tree) : base(tree)
        {
        }

        public override T Translate<T>()
        {
            throw new NotImplementedException();
        }
    }
}
