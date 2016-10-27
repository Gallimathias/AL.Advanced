using Nav_API.Interfaces;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;

namespace Nav_API.NAV_Database
{
    internal class Object
    {
        public Metadata Metadata { get; private set; }
        private IObjectRecord objectRecord;
        
        public Object(IObjectRecord objectRecord)
        {
            this.objectRecord = objectRecord;
            Metadata = new Metadata(objectRecord.ObjectMetadata.First());
            
        }
    }
}