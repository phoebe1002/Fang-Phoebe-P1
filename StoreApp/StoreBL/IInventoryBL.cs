using System;
using System.Collections.Generic;
using StoreModels;
namespace StoreBL
{
    public interface IInventoryBL
    {
        Inventory AddInventory(Location location, Product product, Inventory inventory);
        Dictionary<Product, Inventory> GetProductWithInventory(Location location);
        List<Inventory> GetInventories(Location location);
        
    }
}