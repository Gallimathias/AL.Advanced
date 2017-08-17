using Compiler.Core.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.Core.Translators
{
    public abstract class Translator
    {
        public Translator(SyntaxTree tree)
        {

        }

        public abstract T Translate<T>() where T : SyntaxTree;
    }
}
