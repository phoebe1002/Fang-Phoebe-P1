using System.Collections.Generic;
using StoreModels;
using StoreDL;

namespace StoreBL
{
    public interface ILocationBL
    {
        List<Location> GetAllLocations();
        Location GetLocation(Location location);
        
        Location GetLocationById(int id);
        Location AddLocation(Location location);

    }
}