using StoreModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebUI.Models
{
    public class OrderVM
    {
        public OrderVM()
        {}
        public OrderVM(Order order)
        {
            Id = order.Id;
            OrderDate = order.OrderDate;
            Total = order.Total;
            LocationId =order.LocationId;
            CustomerId = order.CustomerId;
        }
        public int Id { get; set; }
        public decimal Total { get; set; }
        public DateTime OrderDate { get; set; }
        public List<Item> Items { get; set; }
        public int CustomerId { get; set; }
        public int LocationId { get; set; }

        public List<Inventory> Stock { get; set; }
    }
}
