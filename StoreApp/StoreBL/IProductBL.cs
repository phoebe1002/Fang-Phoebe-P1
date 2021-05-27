using System;
using System.Collections.Generic;
using StoreModels;
namespace StoreBL
{
    public interface IProductBL
    {
        Product GetProduct(Product product);
        Product GetProductById(int id);
        Product AddProduct(Product product);
        List<Product> GetAllProducts();
        List<Product> GetAvailableProducts(Location location);
        Dictionary<Product, Inventory> GetProductWithInventory(Location location);
        //HashSet<Item> GetAvaliableProducts(Location location);
        //Product GetProductById(int id);
    }
}