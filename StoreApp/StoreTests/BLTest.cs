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

            Assert.NotNull(result);
            Assert.Equal(5, result[0].Quantity);
        }
    }
}