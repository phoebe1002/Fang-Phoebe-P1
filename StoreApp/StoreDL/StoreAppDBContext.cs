using Microsoft.EntityFrameworkCore;
using StoreModels;

//#nullable disable

namespace StoreDL
{
    public partial class StoreAppDBContext : DbContext
    {
        public StoreAppDBContext() : base()
        {}
        public StoreAppDBContext(DbContextOptions options) : base(options)
        {}


        //Declaring entities
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Inventory> Inventories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Item> Items { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           modelBuilder.Entity<Location>()
           .Property(location => location.Id)
           .ValueGeneratedOnAdd();
           
           modelBuilder.Entity<Inventory>()
           .Property(inventory => inventory.Id)
           .ValueGeneratedOnAdd();

           modelBuilder.Entity<Product>()
           .Property(product => product.Id)
           .ValueGeneratedOnAdd();

           modelBuilder.Entity<Customer>()
           .Property(customer => customer.Id)
           .ValueGeneratedOnAdd();

           modelBuilder.Entity<Order>()
           .Property(order => order.Id)
           .ValueGeneratedOnAdd();

           modelBuilder.Entity<Item>()
           .Property(item => item.Id)
           .ValueGeneratedOnAdd();
        }
    }
}
