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
                .ToDictionary(i => i.Product);
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

        public Order AdddOrder(Customer customer, Location location, Order order)
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
        public Order GetOrder(Order order)
        {
            Order found = _context.Orders.FirstOrDefault(obj => obj == order);
            if (found == null) return null;
            return new Order(found.Id, found.OrderDate, found.Total);
        }

        public List<Order> GetAllOrders(Customer customer)
        {
            return _context.Orders
            .Where(
                order => order.CustomerId == GetCustomer(customer).Id
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
        public List<Item> GetAllItems(Order order)
        {
            return _context.Items
            .Where(
                item => item.OrderId == GetOrder(order).Id
            ).Select(
                item => item
            ).ToList();
        }
    }
}