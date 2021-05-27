using Xunit;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using StoreDL;
using System.Collections.Generic;
using StoreModels;
using System;
using Model = StoreModels;


namespace StoreApp.StoreTests
{
    public class RepoTest
    {
        private readonly DbContextOptions<StoreAppDBContext> options;
        public RepoTest()
        {
            options = new DbContextOptionsBuilder<StoreAppDBContext>().UseSqlite("Filename=Test.db").Options;
            Seed();
        }
        /// <summary>
        /// Location
        /// </summary>
        [Fact]
        public void GetAllBranchLocationsShouldReturnRightCount()
        {
            using (var context = new StoreAppDBContext(options))
            {
                IRepository _repo = new RepoDB(context);

                List<Location> result = _repo.GetAllLocations();

                Assert.NotNull(result);
                Assert.Equal(2, result.Count);
            }
        }  

        [Fact]
        public void GetAllInventoriesShouldReturnRightProductName()
        {
            using (var context = new StoreAppDBContext(options))
            {
                IRepository _repo = new RepoDB(context);

                List<Inventory> result = _repo.GetInventories(_repo.GetLocationById(1));
                
                Assert.Equal(2, result.Count);
                Assert.Equal("Classic Milk", result[0].Product.Name);
            }
        }

        [Fact]
        public void GetLocationShouldReturnRightName()
        {
            using (var context = new StoreAppDBContext(options))
            {
                IRepository _repo = new RepoDB(context);
                var setup = new Location(1, "WA-Seattle", "2245 8th Ave., R1-1A Seattle WA 98121");
                
                var result = _repo.GetLocation(setup);

                Assert.NotNull(result);
                Assert.Equal("WA-Seattle", result.Name);
            }
        }   

        [Fact]
        public void AddLocationShouldReturnRightName()
        {
            using (var context = new StoreAppDBContext(options))
            {
                IRepository _repo = new RepoDB(context);
                _repo.AddLocation(
                    new Model.Location("WA-Bellevue", "2245 8th Ave., R1-1A Seattle WA 98121")
                );
            }
            using (var assertContext = new StoreAppDBContext(options))
            {
                var result = assertContext.Locations.FirstOrDefault(obj => obj.Id == 3);

                Assert.NotNull(result);
                Assert.Equal("WA-Bellevue", result.Name);
            }
        }

        [Fact]
        public void AddInventoryShouldReturnRightQuantity()
        {
            using (var context = new StoreAppDBContext(options))
            {
                IRepository _repo = new RepoDB(context);
                _repo.AddInventory( _repo.GetLocationById(1), _repo.GetProductById(3),
                    new Inventory{
                        Id = 3,
                        Quantity = 5
                    }
                );
            }
            using (var assertContext = new StoreAppDBContext(options))
            {
                var result = assertContext.Inventories.FirstOrDefault(obj => obj.Id == 3);
                Assert.NotNull(result);
                Assert.Equal(5, result.Quantity);
            }
        }


        /// <summary>
        /// Customer
        /// </summary>
        [Fact]
        public void AddCustomerShoudReturnRightFirstName()
        {
            using (var context = new StoreAppDBContext(options))
            {
                IRepository _repo = new RepoDB(context);
                _repo.AddCustomer(
                    new Model.Customer("Jia", "W", "Fang", "2063317069")
                );
            }
            using (var assertContext = new StoreAppDBContext(options))
            {
                var result = assertContext.Customers.FirstOrDefault(obj => obj.Id == 2);

                Assert.NotNull(result);
                Assert.Equal("Jia", result.FirstName);
            }
        }
        public void Seed()
        {
            using (var context = new StoreAppDBContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
                //Seed location data
                context.Locations.AddRange(
                    new Location
                    {
                        Id = 1,
                        Name = "WA-Seattle",
                        Address = "2245 8th Ave., R1-1A Seattle WA 98121"
                    },
                    new Location
                    {
                        Id = 2,
                        Name = "WA-Sammamish",
                        Address = "1171 NW Sammamish Road, Suite 109 Issaquah WA 98027"
                    }
                );

                context.Products.AddRange(
                    new Product
                    {
                        Id = 1,
                        Name = "Classic Milk",
                        Price = 4.99m,
                        Description = "low calories"
                    },
                    new Product
                    {
                        Id = 2,
                        Name = "Boba Milk",
                        Price = 3.99m,
                        Description = "high calories"
                    },
                    new Product
                    {
                        Id = 3,
                        Name = "Jasmine Milk",
                        Price = 5.99m,
                        Description = "no calories"
                    }
                );
                
                context.Inventories.AddRange(
                    new Inventory
                    {
                        Id = 1,
                        Quantity = 10,
                        ProductId = 1,
                        LocationId = 1
                    }, 
                    new Inventory
                    {
                        Id = 2,
                        Quantity = 5,
                        ProductId = 2,
                        LocationId = 1
                    }
                );

                //Seed customer data
                context.Customers.AddRange
                (
                    new Customer
                    {
                        Id = 1,
                        LastName = "Yang",
                        MiddleName = "",
                        FirstName = "Jessie",
                        PhoneNumber = "2067798888"
                    },
                    new Customer
                    {
                        Id = 2,
                        LastName = "Fang",
                        MiddleName = "W",
                        FirstName = "Jia",
                        PhoneNumber = "2063317069"
                    }
                );
                context.SaveChanges();
            }
        }
    }
}