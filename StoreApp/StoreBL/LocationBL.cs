using System;
using System.Collections.Generic;
using StoreModels;
using StoreDL;
namespace StoreBL
{
    public class LocationBL : ILocationBL
    {
        private IRepository _repo;
        public LocationBL(IRepository repo)
        {
            this._repo = repo;
        }
        public List<Location> GetAllLocations()
        {
            //ToDo: get locations from DL
            return _repo.GetAllLocations();
        }
        public Location GetLocation(Location location)
        {
            return _repo.GetLocation(location);
        }

        public Location GetLocationById(int id)
        {
            return _repo.GetLocationById(id);
        }
        public Location AddLocation(Location location)
        {
            if(_repo.GetLocation(location) != null)
            {
                throw new Exception("Branch already exists");
            }
            return _repo.AddLocation(location);
        }
    }
}
