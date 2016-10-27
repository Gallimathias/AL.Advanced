using Nav_API.NAV_Database;
using Nav_API.SQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nav_API
{
    public static class Manager
    {
        public static Database GetDatabase(string connection) 
            => new Database(new Nav_DatabaseDataContext(connection));

    }
}
