using System;
using System.Collections.Generic;

namespace StoreModels
{
    /// <summary>
    /// Data structure used to define an order
    /// </summary>
    public class Order
    {
        public Order()
        {}
        public Order(DateTime orderdate)
        {
            this.OrderDate = orderdate;
        }
        public Order(DateTime orderdate, decimal total) : this(orderdate)
        {
            this.Total = total;
        }
        public Order(int id, DateTime orderdate, decimal total) : this(orderdate, total)
        {
            this.Id = id;
        }

        public Order(int id, int customerId, int locationId, DateTime orderdate, decimal total) : this(id, orderdate, total)
        {
            this.CustomerId = customerId;
            this.LocationId = locationId;
        }
        public int Id { get; set; }
        public decimal Total { get; set; }
        public DateTime OrderDate { get; set; }
        public List<Item> Items { get; set; }
        public int CustomerId { get; set; }
        public int LocationId { get; set; }
        
        // public Customer Customer { get; set; }
        // public Location Location { get; set; }
        

        //ToDo: view details of an order
        // public override string ToString()
        // {
        //     return $"\tCustomer Name: {Customer.FullName}\tBranch Location: {Location.Name}\tOrderId: {Id}\tOrderDate: {OrderDate.ToString("yyyy-MM-dd")}\tTotal: ${Total}";
        // }
    }
}