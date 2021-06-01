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
        [Fact]
        public void GetAvailableProductsShouldReturnProductsWithoutInventoryAdded()
        {
            var mockRepo = new Mock<IRepository>();
            mockRepo.Setup(x => x.GetInventories(It.IsAny<Location>())).Returns
            (
                new List<Inventory>()
                {
                    new Inventory()
                    {
                        Quantity = 5,
                        Product = new Product()
                        {
                            Id = 1,
                            Name = "Boba",
                            Price = 5.99m,
                            Description = "good"
                        }
                    }
                }
            );

            var inventoryBL = new InventoryBL(mockRepo.Object);
            var result = inventoryBL.GetInventories(new Location());

            var product = new Product()
            {
                Id = 2,
                Name = "Classic",
                Price = 5.99m,
                Description = "good"
            };
            Assert.Equal(5, result[0].Quantity);
        }

        [Fact]
        public void UpdateInventory()
        {
            var mockRepo = new Mock<IRepository>();
            mockRepo.Setup(x => x.AddInventory(It.IsAny<Location>(), It.IsAny<Product>(), It.IsAny<Inventory>())).Returns
            (
                new Inventory()
                {
                    Id = 1,
                    Quantity = 5,
                    Product = new Product()
                    {
                        Id = 1,
                        Name = "Boba",
                        Price = 5.99m,
                        Description = "good"
                    }
                }
            );
            var inventoryBL = new InventoryBL(mockRepo.Object);
            var result = inventoryBL.UpdateInventory(new Inventory(1, 4));
            Assert.Equal(5, result.Quantity);
        }
    }
}