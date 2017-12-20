using Nav.API.Database;
using Nav.API.SQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nav.API
{
    public static class Manager
    {
        public static Database.Database GetDatabase(string connection) 
            => new Database.Database(new Nav_DatabaseDataContext(connection));

    }
}
