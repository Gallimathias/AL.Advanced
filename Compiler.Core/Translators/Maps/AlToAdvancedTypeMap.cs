using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlType = Compiler.Core.Syntax.AL.Types;
using AlAdvancedType = Compiler.Core.Syntax.ALAdvanced.Types;

namespace Compiler.Core.Translators.Maps
{
    public class AlToAdvancedTypeMap : Map
    {
        public AlToAdvancedTypeMap(AlToAdvanced translator) : base(translator)
        {
        }

        public override object Invoke(string typeName, object type) => GetType().GetMethod(typeName).Invoke(this, new[] { type });

        public AlAdvancedType.IntegerSyntax IntegerSyntax(AlType.IntegerSyntax integerSyntax) => Copy<AlAdvancedType.IntegerSyntax>(integerSyntax);
    }
}
