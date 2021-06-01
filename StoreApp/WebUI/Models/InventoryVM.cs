using StoreModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebUI.Models
{
    public class InventoryVM
    {
        public InventoryVM()
        {}
        public InventoryVM(int locationId, int productId)
        {
            LocationId = locationId;
            ProductId = productId;
        }

        public InventoryVM(Inventory inventory)
        {
            Id = inventory.Id;
            Quantity = inventory.Quantity;
            LocationId = inventory.LocationId;
            ProductId = inventory.ProductId;
        }
        
        public int Id { get; set; }

        [Required]
        [Range(1, 100)]
        public int Quantity { get; set; }

        public int ProductId { get; set; }
        public int LocationId { get; set; }
        public Product Product { get; set; }
        
    }
}