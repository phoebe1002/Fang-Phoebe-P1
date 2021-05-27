using System.Collections.Generic;
using StoreModels;
namespace StoreDL
{
    public interface IRepository
    {
        Product GetProduct(Product product);
        Product GetProductById(int id);
        Product AddProduct(Product product);
        List<Product> GetAllProducts();
    
        List<Location> GetAllLocations();
        Location GetLocation(Location location);
        Location GetLocationById(int id);
        Location AddLocation(Location location);
        // Customer UpdateLocation(Location location);
        // Customer DeleteLocation(Location location);
        
        Inventory AddInventory(Location location, Product product, Inventory inventory);
        List<Inventory> GetInventories(Location location);
        Dictionary<Product, Inventory> GetProductWithInventory(Location location);
      
        List<Customer> GetAllCustomers();
        Customer GetCustomer(Customer customer);
        Customer GetCustomerById(int id);
        Customer AddCustomer(Customer Customer);
        Customer UpdateCustomer(Customer customer);
        Customer DeleteCustomer(Customer customer);

        Order AdddOrder(Customer customer, Location location, Order order);
        public Order GetOrder(Order order);
        List<Order> GetAllOrders(Customer customer);

        Item AddItem(Order order, Product product, Item item);
        List<Item> GetAllItems(Order order);

    }
}