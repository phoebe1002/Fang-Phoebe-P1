using StoreModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebUI.Models
{
    public class CartVM
    {
        private int _quantity = 0;
        public CartVM()
        {}
        public CartVM(Cart cart)
        {
            Id = cart.Id;
            LocationId = cart.LocationId;
            CustomerId = cart.CustomerId;
            InventoryId = cart.InventoryId;
            ProductId = cart.ProductId;
            Quantity = cart.Quantity;
            Price = cart.Price;
            Name = cart.Name;
        }
        public int Id { get; set; }
        public int TempId { get; set; }
        public int CustomerId{ get; set; }
        public int LocationId{ get; set; }
        public int InventoryId{ get; set; }
        public int ProductId{ get; set; }

        [Required]
        [Range(1, 100)]
        public int Quantity { get; set;}
        public decimal Price { get; set; }
        public string Name { get; set; }
        public decimal Total { get; set; }
    }
}