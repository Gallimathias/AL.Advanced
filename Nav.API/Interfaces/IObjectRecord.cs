using System.Collections.Generic;
using System.Data.Linq;

namespace Nav.API.Interfaces
{
    public interface IObjectRecord
    {

        int Type { get; set; }
        int ID { get; set; }
        IEnumerable<IObjectMetadata> ObjectMetadata { get; set; }


    }
}