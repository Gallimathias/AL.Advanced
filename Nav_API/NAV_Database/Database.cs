using Nav_API.Interfaces;
using Nav_API.SQL;
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nav_API.NAV_Database
{
    public class Database
    {
        public NavObjectHandler<Table<IObjectRecord>> Objects { get; private set; }
        private IDatabaseContext database;

        public Database(IDatabaseContext context)
        {
            database = context;
            Objects = new NavObjectHandler<Table<IObjectRecord>>(context.GetTable<IObjectRecord>());
            
        }
    }
}
