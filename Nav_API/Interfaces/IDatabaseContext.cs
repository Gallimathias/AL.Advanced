using System.Data.Linq;

namespace Nav_API.Interfaces
{
    public interface IDatabaseContext
    {        
        Table<TEntity> GetTable<TEntity>() where TEntity : class;
    }
}