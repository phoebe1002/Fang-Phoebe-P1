using System.Collections.Generic;
using StoreModels;
using StoreDL;
using System;
using System.Linq;

namespace StoreBL
{
    public class ProductBL : IProductBL
    {
        private IRepository _repo;
        public ProductBL()
        {
        }

        public ProductBL(IRepository repo)
        {
            this._repo = repo;
        }

        public Product GetProduct(Product product)
        {
            return _repo.GetProduct(product);
        }

        public Product GetProductById(int id)
        {
            return _repo.GetProductById(id);
        }
        public Product AddProduct(Product product)
        {
            if (GetProduct(product) != null)
            {
                throw new Exception("The product barcode is already in our system. Please check again");
            }
            return _repo.AddProduct(product);
        }

        public List<Product> GetAllProducts()
        {
            return _repo.GetAllProducts();
        }

        public List<Product> GetAvailableProducts(Location location)
        {
            List<Product> products = _repo.GetAllProducts();
            List<Product> availableProducts = new List<Product>();
            Dictionary<Product, Inventory> productWithInventory = _repo.GetProductWithInventory(location);
            foreach(Product product in products)
            {
                if (!productWithInventory.Keys.Contains(product))
                {
                    availableProducts.Add(product);
                }
            }
            return availableProducts;
        }

        public Dictionary<Product, Inventory> GetProductWithInventory(Location location)
        {
            return _repo.GetProductWithInventory(location);
        }

        // public Product GetProductById(int id)
        // {
        //     return _repo.GetProductById(id);
        // }

        // public HashSet<Item> GetAvaliableProducts(Location location)
        // {
        //     HashSet<Item> inventories = _repo.GetAllInventories(location);
        //     return inventories.Where(o => o.Quantity > 0).ToHashSet();
        // }

    }
}