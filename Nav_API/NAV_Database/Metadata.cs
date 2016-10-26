using Nav_API.Interfaces;
using System.Data.Linq;
using System.Linq;
using System.Collections.Generic;

namespace Nav_API.NAV_Database
{
    internal class Metadata
    {
        private IObjectMetadata metadata;
        

        public Metadata(IObjectMetadata object_metadata)
        {
            metadata = object_metadata;
        }
        
    }
}