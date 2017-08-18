using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AL
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class IDAttribute : Attribute
    {
        public int ID { get; private set; }

        public IDAttribute(int id)
        {
            ID = id;
        }
    }
}
