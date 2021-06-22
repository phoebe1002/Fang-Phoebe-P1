using Xunit;
using System.Linq;
using StoreDL;
using StoreBL;
using System.Collections.Generic;
using StoreModels;
using System;
using Moq;

namespace StoreTests
{
    public class BLTest
    {
        Mock<IRepository> mockRepo;
        IInventoryBL inventoryBL;
        IOrderBL orderBL;
        ILocationBL locationBL;
        ICustomerBL customerBL;
        List<Inventory> inventories;
        Tuple<List<Cart>, decimal> cartItems;

        List<Inventory> AvailableProductByInventory;
        Location location;
        Customer customer;

        public BLTest()
        {
            mockRepo = new Mock<IRepository>();
            mockRepo.Setup(x => x.GetInventories(It.IsAny<Location>())).Returns
            (
                new List<Inventory>()
                {
                    new Inventory()
                    {
                        Id = 1,
                        Quantity = 5,
                        LocationId = 1,
                        ProductId = 1
                    },
                    new Inventory()
                    {
                        Id = 2,
                        Quantity = 1,
                        LocationId = 1,
                        ProductId = 2
                    }
                }
            );
            inventoryBL = new InventoryBL(mockRepo.Object);
            inventories = inventoryBL.GetInventories(new Location());

            mockRepo.Setup(x => x.GetAllCartItems(It.IsAny<int>())).Returns
            (
                new List<Cart>()
                {
                    new Cart()
                    {
                    Id = 1,
                    LocationId = 1,
                    CustomerId = 1,
                    InventoryId = 1,
                    ProductId = 1,
                    Quantity = 2,
                    Price = 2.99m,
                    Name = "Classic"
                    }
                }
            );

            orderBL = new OrderBL(mockRepo.Object);
            cartItems = orderBL.GetAllCartItems(1);

            mockRepo.Setup(x => x.GetLocation(It.IsAny<Location>())).Returns
            (
                new Location()
                {
                    Id = 1,
                    Name = "WA-Seattle", 
                    Address = "123"
                }
            );

            locationBL = new LocationBL(mockRepo.Object);
            location = locationBL.GetLocation(new Location());

            mockRepo.Setup(x => x.GetCustomer(It.IsAny<Customer>())).Returns
            (
                new Customer()
                {
                    Id = 1,
                    FirstName = "Phoebe",
                    MiddleName = "",
                    LastName = "Fang",
                    PhoneNumber = "2063317069"
                }
            );

            customerBL = new CustomerBL(mockRepo.Object);
            customer = customerBL.GetCustomer(new Customer());

            AvailableProductByInventory = inventoryBL.GetAvaliableProductsByInventory(location);
        }

        [Fact]
        public void GetCustomerIsNotNull()
        {
            Assert.NotNull(customer);
        }
        [Fact]
        public void GetCustomerShouldReturnRightName()
        {
            Assert.Equal("Phoebe", customer.FirstName);
        }
        
        [Fact]
        public void GetLocationIsNotNull()
        {
            Assert.NotNull(location);
        }
        [Fact]
        public void GetLocationShouldReturnRightName()
        {
            Assert.Equal("WA-Seattle", location.Name);
        }

        [Fact]
        public void GetInventoriesIsNotNull()
        {
            Assert.NotNull(inventories);
        }

        [Fact]
        public void GetInventoriesShouldReturnRigthtCount()
        {
            Assert.Equal(2, inventories.Count);
        }

        [Fact]
        public void GetInventoriesShouldReturnRightItem()
        {
            Assert.Equal(5, inventories[0].Quantity);
        }

        [Fact]
        public void GetCartItemsShouldReturnRigthtCount()
        {
            Assert.Equal(1, cartItems.Item1.Count);
        }

        [Fact]
        public void GetCartItemsIsNotNull()
        {
            Assert.NotNull(cartItems);
        }

        [Fact]
        public void GetCartItemsShouldReturnRightItem()
        {
            Assert.Equal(2, cartItems.Item1[0].Quantity);
        }

         [Fact]
        public void GetAvaliableProductsByInventoryIsNotNull()
        {
            Assert.NotNull(AvailableProductByInventory);
        }

        [Fact]
        public void GetAvaliableProductsByInventoryShouldReturnRightCount()
        {
            Assert.Equal(2, AvailableProductByInventory.Count);
        }

        [Fact]
        public void GetAvaliableProductsByInventoryShouldReturnRightItem()
        {
            Assert.Equal(5, AvailableProductByInventory[0].Quantity);
        }

        [Fact]
        public void GetUnselectedProductsShouldRightRightCount()
        {
            List<Inventory> result = new List<Inventory>();
            foreach(Inventory inventory in inventories)
            {
                foreach(Cart cart in cartItems.Item1)
                {
                    if (cart.InventoryId != inventory.Id)
                    {
                        result.Add(inventory);
                    }
                }
            }
            Assert.Equal(1, result.Count);
        }
        
    }
}