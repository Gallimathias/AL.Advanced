using Nav.API.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nav.API
{
    public class NavObjectHandler<TTable> where TTable : ITable<IObjectRecord>
    {
        private TTable table;

        public IObjectRecord this[int type, int id] => table.FirstOrDefault(a => a.Type == type && a.ID == id);

        public NavObjectHandler(TTable table)
        {
            this.table = table;
        }
    }
}
