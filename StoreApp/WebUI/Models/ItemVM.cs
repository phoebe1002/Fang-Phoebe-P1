using StoreModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebUI.Models
{
    public class ItemVM
    {
        public ItemVM()
        {}

        public ItemVM(int id, int quantity)
        {
            Id = id;
            Quantity = quantity;
        }

        public int Id { get; set; }
        [Required]
        [Range(0, 100)]
        public int Quantity { get; set; }

        public int ProductId { get; set; }
        public int OrderId { get; set; }

        public int InventoryId { get; set; }
        public int CustomerId { get; set; }
        public int LocationId { get; set; }
        public Product Product { get; set; }
        public decimal Amount { get; set; }

    }
}