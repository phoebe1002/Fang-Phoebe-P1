using System;
using System.Collections.Generic;
using StoreModels;
namespace StoreBL
{
    public interface IInventoryBL
    {
        Inventory AddInventory(Location location, Product product, Inventory inventory);
        Inventory UpdateInventory(Inventory inventory);
        Inventory GetInventoryById(int id);
        Dictionary<Product, Inventory> GetProductWithInventory(Location location);
        List<Inventory> GetAvaliableProductsByInventory(Location location);
         List<Inventory> GetUnselectedProducts(Location location, int customerId);
        List<Inventory> GetInventories(Location location);   
        List<Cart> GetAllCartItems(int customerId);
        Cart GetCartItemByInventoryId(int inventoryId);
    }
}