using System.Collections.Generic;
using StoreModels;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace StoreDL
{
    public class RepoDB : IRepository
    {
        private StoreAppDBContext _context;
        public RepoDB(StoreAppDBContext context)
        {
            _context = context;
        }

        public Product GetProduct(Product product)
        {
            Product found = _context.Products.FirstOrDefault(obj => obj.Id == product.Id);
            if (found == null) return null;
            return new Product(found.Id, found.Name, found.Price, found.Description);
        }

        public Product GetProductById(int id)
        {
            return _context.Products.Find(id);
        }
        public Product AddProduct(Product product)
        {
            _context.Products.Add(
                product
            );
            _context.SaveChanges();
            return product;
        }
        public List<Product> GetAllProducts()
        {
            return _context.Products
            .Select(
                product => product
            ).ToList();
        }

        public List<Location> GetAllLocations()
        {
            return _context.Locations
            .Select(
                location => location
            ).ToList();
        }
        public Location GetLocation(Location location)
        {
            Location found = _context.Locations.FirstOrDefault(obj => obj.Id == location.Id);
            if (found == null) return null;
            return new Location(found.Id, found.Name, found.Address);
        }
        public Location GetLocationById(int id)
        {
            return _context.Locations.Find(id);
        }
        public Location AddLocation(Location location)
        {
            _context.Locations.Add(
                location
            );
            _context.SaveChanges();
            return location;
        }

        public Inventory AddInventory(Location location, Product product, Inventory inventory)
        {
            _context.Inventories.Add(
                new Inventory
                {
                    Quantity = inventory.Quantity,
                    ProductId = GetProduct(product).Id,
                    LocationId = GetLocation(location).Id
                }
            );
            _context.SaveChanges();
            return inventory;
        }
        public Inventory UpdateInventory(Inventory inventory)
        {
            _context.Inventories.Update(inventory);
            _context.SaveChanges();
            return inventory;
        }

        public Inventory GetInventoryById(int id)
        {   
            return _context.Inventories.Find(id);
        }
        public List<Inventory> GetInventories(Location location)
        {
            return _context.Inventories
            .Where(
                inventory => inventory.LocationId == GetLocation(location).Id
            ).Select(
                Inventory => Inventory
            ).ToList();
        }

        public Dictionary<Product, Inventory> GetProductWithInventory(Location location)
        {
            return _context.Inventories.Where(
                inventory => inventory.LocationId == GetLocation(location).Id)
                .Select(
                    inventory => new Inventory()
                    {
                        Quantity = inventory.Quantity,
                        Product = _context.Products.FirstOrDefault(obj => obj.Id == inventory.ProductId)
                    })
                .ToList()
                .ToDictionary(key => key.Product);
        }

        public List<Customer> GetAllCustomers()
        {
            return _context.Customers
            .Select(
                customer => customer
            ).ToList();
        }
        public Customer GetCustomer(Customer customer)
        {
            Customer found = _context.Customers.FirstOrDefault(obj => obj == customer);
            if (found == null) return null;
            return new Customer(customer);
        }
        public Customer GetCustomerById(int id)
        {
            return _context.Customers.Find(id);
        }

        public Customer GetCustomerByPhone(string phone)
        {
            Customer found = _context.Customers.FirstOrDefault(obj => obj.PhoneNumber == phone);
            if (found == null) return null;
            return new Customer(found);
        }

        public Customer AddCustomer(Customer customer)
        {
            _context.Customers.Add(
                customer
            );
            _context.SaveChanges();
            return customer;
        }
        public Customer UpdateCustomer(Customer customer)
        {
            _context.Customers.Update(customer);
            _context.SaveChanges();
            return customer;
        }
        public Customer DeleteCustomer(Customer customer)
        {
            Customer toBeDeleted = _context.Customers.First(obj => obj.Id == obj.Id);
            _context.Customers.Remove(toBeDeleted);
            _context.SaveChanges();
            return customer;
        }

        public Order AddOrder(Customer customer, Location location, Order order)
        {
            _context.Orders.Add(
                new Order
                {
                    OrderDate = order.OrderDate,
                    Total = order.Total,
                    CustomerId = customer.Id,
                    LocationId = location.Id
                }
            );
            _context.SaveChanges();
            return order;
        }

        public Order AddOrder(Order order)
        {
            _context.Orders.Add(
                new Order
                {
                    OrderDate = order.OrderDate,
                    Total = order.Total,
                    CustomerId = order.CustomerId,
                    LocationId = order.LocationId
                }
            );
            
            _context.SaveChanges();
            return order;
        }

        public Order GetOrder(Order order)
        {
            Order found = _context.Orders.FirstOrDefault(o => 
                o.CustomerId == order.CustomerId &&
                o.LocationId == order.LocationId &&
                o.OrderDate == order.OrderDate &&
                o.Total == order.Total
            );
            if (found == null) return null;
            return new Order(found.Id, found.OrderDate, found.Total);
        }

        public List<Order> GetAllOrdersByCustomer(Customer customer)
        {
            return _context.Orders
            .Where(
                order => order.CustomerId == GetCustomer(customer).Id
            ).Select(
                order => order
            ).ToList();
        }
        
        public List<Order> GetAllOrdersByCustomer(int customerId)
        {
            return _context.Orders
            .Where(
                order => order.CustomerId == customerId
            ).Select(
                order => order
            ).ToList();
        }
        
        public Item AddItem(Order order, Product product, Item item)
        {
            _context.Items.Add(
                new Item
                {
                    Quantity = item.Quantity,
                    ProductId = GetProduct(product).Id,
                    OrderId = GetOrder(order).Id
                }
            );
            _context.SaveChanges();
            return item;
        }

        public Item AddItem(Item item)
        {
            _context.Items.Add(
                new Item
                {
                    Quantity = item.Quantity,
                    ProductId = item.ProductId,
                    OrderId = item.OrderId
                }
            );
            _context.SaveChanges();
            return item;
        }
        public List<Item> GetAllItems(Order order)
        {
            return _context.Items
            .Where(
                item => item.OrderId == GetOrder(order).Id
            ).Select(
                item => item
            ).ToList();
        }

        public List<Item> GetOrderItemByOrderId(int orderId)
        {
            return _context.Items
            .Where(
                item => item.OrderId == orderId
            ).Select(
                item => item
            ).ToList();
        }

        public Cart GetCartItem(int id)
        {
            return _context.Cart.Find(id);
        }
        
        public Cart GetCartItemByInventoryId(int inventoryId)
        {
            return _context.Cart.FirstOrDefault(c => c.InventoryId == inventoryId);
        }
        public List<Cart> GetAllCartItems(int customerId)
        {
            return _context.Cart
            .Where(
                item => item.CustomerId == GetCustomerById(customerId).Id
            ).Select(
                cart => new Cart{
                    Id = cart.Id,
                    LocationId = cart.LocationId,
                    CustomerId = cart.CustomerId,
                    InventoryId = cart.InventoryId,
                    ProductId = cart.ProductId,
                    Quantity = cart.Quantity,
                    Price = cart.Price,
                    Name = cart.Name
                }
            ).ToList();
        }
        public Cart UpdateCartItem(Cart cart)
        {
            _context.Cart.Update(cart);
            _context.SaveChanges();
            return cart;
        }
        public Cart AddToCart(Cart cart)
        {
            _context.Cart.Add(
                new Cart
                {
                    LocationId = cart.LocationId,
                    CustomerId = cart.CustomerId,
                    InventoryId = cart.InventoryId,
                    ProductId = cart.ProductId,
                    Quantity = cart.Quantity,
                    Price = cart.Price,
                    Name = cart.Name
                }
            );
            _context.SaveChanges();
            return cart;
        }

        public Cart DeleteCartItem(Cart cart)
        {
            Cart itemToBeDelete = _context.Cart.First(i => i.Id == cart.Id);
            _context.Cart.Remove(itemToBeDelete);
            _context.SaveChanges();
            return cart;
        }

        public void DeleteCart(int customerId)
        {
            List<Cart> cartItems = GetAllCartItems(customerId);
            foreach(Cart i in cartItems)
            {
                if (i.CustomerId == customerId)
                {
                    DeleteCartItem(i);
                }
            }
        }
        public void AddToCart(Customer customer, Location location, Inventory inventory, Product product, Item item)
        {
            Product p = GetProductById(product.Id);
            _context.Cart.Add(
                new Cart
                {
                    LocationId = GetLocationById(location.Id).Id,
                    CustomerId = GetCustomerById(customer.Id).Id,
                    InventoryId = GetInventoryById(inventory.Id).Id,
                    ProductId = p.Id,
                    Quantity = item.Quantity,
                    Price = p.Price,
                    Name = p.Name
                }
            );
            _context.SaveChanges();
        }

    }
}