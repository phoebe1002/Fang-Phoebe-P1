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

        public Inventory UpdateInventory(Inventory inventory)
        {
            return _repo.UpdateInventory(inventory);
        }

        public Inventory GetInventoryById(int id)
        {
            return _repo.GetInventoryById(id);
        }
        
        public Dictionary<Product, Inventory> GetProductWithInventory(Location location)
        {
            return _repo.GetProductWithInventory(location);
        }

        public List<Inventory> GetInventories(Location location)
        {
            return _repo.GetInventories(location);
        }

        public List<Inventory> GetAvaliableProductsByInventory(Location location)
        {
            List<Inventory> inventories = _repo.GetInventories(location);
            return inventories.Where(o => o.Quantity > 0).ToList();
        }

        public List<Inventory> GetUnselectedProducts(Location location, int customerId)
        {
            List<Cart> cartItems = _repo.GetAllCartItems(customerId);
            List<Inventory> inventories = GetAvaliableProductsByInventory(location);
            List<Inventory> result = new List<Inventory>();
            foreach(Inventory inventory in inventories)
            {
                if (!cartItems.Contains(_repo.GetCartItemByInventoryId(inventory.Id)))
                {
                    result.Add(inventory);
                }
            }
            return result;
        }
    }
}