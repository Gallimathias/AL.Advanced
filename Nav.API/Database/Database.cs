using Nav.API.Interfaces;
using System.Data.Linq;

namespace Nav.API.Database
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
