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
        Inventory UpdateInventory(Inventory inventory);
        Inventory GetInventoryById(int id);
        List<Inventory> GetInventories(Location location);
        Dictionary<Product, Inventory> GetProductWithInventory(Location location);
      
        List<Customer> GetAllCustomers();
        Customer GetCustomer(Customer customer);
        Customer GetCustomerById(int id);
        Customer AddCustomer(Customer Customer);
        Customer UpdateCustomer(Customer customer);
        Customer DeleteCustomer(Customer customer);
        Customer GetCustomerByPhone(string phone);

        Order AddOrder(Customer customer, Location location, Order order);
        Order AddOrder(Order order);
        Order GetOrder(Order order);
        
        List<Order> GetAllOrdersByCustomer(int customerId);

        Item AddItem(Order order, Product product, Item item);
        List<Item> GetAllItems(Order order);
        List<Item> GetOrderItemByOrderId(int orderId);

        void AddToCart(Customer customer, Location location, Inventory inventory, Product product, Item item);
        Cart AddToCart(Cart cart);
        Cart GetCartItem(int id);
        Cart GetCartItemByInventoryId(int inventoryId);
        List<Cart> GetAllCartItems(int customerId);
        Cart UpdateCartItem(Cart cart);
        Cart DeleteCartItem(Cart cart);
        Item AddItem(Item item);
        void DeleteCart(int customerId);

    }
}