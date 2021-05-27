using System;
using System.Collections.Generic;
using StoreModels;
using StoreDL;
using System.Linq;

namespace StoreBL
{
    public class InventoryBL : IInventoryBL
    {
        private IRepository _repo;
        public InventoryBL(IRepository repo)
        {
            this._repo = repo;
        }
        public Inventory AddInventory(Location location, Product product, Inventory inventory)
        {
            return _repo.AddInventory(location, product, inventory);
        }
        public Dictionary<Product, Inventory> GetProductWithInventory(Location location)
        {
            return _repo.GetProductWithInventory(location);
        }
        public List<Inventory> GetInventories(Location location)
        {
            return _repo.GetInventories(location);
        }

    }
}