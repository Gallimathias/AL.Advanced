using Nav_API;
using Nav_API.NAV_Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.Core
{
    public static class DatabaseManager
    {
        static Dictionary<string, Database> databases;

        public static void Initzalitation()
        {
            databases = new Dictionary<string, Database>();
        }

        public static void AddDatabase(Database database) => databases.Add(database.ToString(), database);

        public static void AddDatabase(string connection)
        {

        }

        public static Database GetDatabase(string connection) => Manager.GetDatabase(connection);

        public static Database OpenDatabaseConnection(string connection)
        {
            var result = GetDatabase(connection);
            AddDatabase(result);
            return result;
        }
    }
}
