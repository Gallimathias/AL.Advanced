using System.Data.Linq;

namespace Nav_API.Interfaces
{
    public interface IObjectMetadata
    {
        Binary User_Code { get; set; }
        Binary User_AL_Code { get; set; }
    }
}