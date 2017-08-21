using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.Core.Translators.Maps
{
    public class AlToAdvancedStatementMap : Map
    {

        public AlToAdvancedStatementMap(AlToAdvanced translator) : base(translator)
        {
        }

        public override object Invoke(string statmentname, object statement) => GetType().GetMethod(statmentname).Invoke(this, new[] { statement });
    }
}
