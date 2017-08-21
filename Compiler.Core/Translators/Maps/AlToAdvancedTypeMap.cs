using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.Core.Translators.Maps
{
    public class AlToAdvancedTypeMap : Map
    {
        public AlToAdvancedTypeMap(AlToAdvanced translator) : base(translator)
        {
        }

        public override object Invoke(string typeName, object type)
        {
            throw new NotImplementedException();
        }
    }
}
