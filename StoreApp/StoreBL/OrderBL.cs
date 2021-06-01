using System.Collections.Generic;
using StoreModels;
using StoreDL;
using System;
using System.Linq;

namespace StoreBL
{
    public class OrderBL : IOrderBL
    {
        private IRepository _repo;
        public OrderBL()
        {}
        public OrderBL(IRepository repo)
                {
            this._repo = repo;
        }

        public Order AddOrder(Customer customer, Location location, Order order)
        {
            //add order
            //add a list of items
            //update inventory
            return _repo.AddOrder(customer, location, order);
        }

        public Order AddOrder(Order order)
        {
            return _repo.AddOrder(order);
        }

        public Item AddItem(Order order, Product product, Item item)
        {
            return _repo.AddItem(order, product, item);
        }

        public Cart AddToCart(Cart cart)
        {
            return _repo.AddToCart(cart);
        }

        public Cart GetCartItem(int id)
        {
            return _repo.GetCartItem(id);
        }
        public Cart UpdateCartItem(Cart cart)
        {
            return _repo.UpdateCartItem(cart);
        }
        public Tuple<List<Cart>, decimal> GetAllCartItems(int customerId)
        {
            List<Cart> cartItems = _repo.GetAllCartItems(customerId);
            decimal total = 0;
            if (cartItems.Count > 0)
            {
                cartItems.ForEach(item => total+= item.Price * item.Quantity);
            }
            return new Tuple<List<Cart>, decimal>(cartItems, total);
        }

        public void AddToCart(Customer customer, Location location, Inventory inventory, Product product, Item item)
        {
            _repo.AddToCart(customer, location, inventory, product, item);
        }
        public Cart DeleteCartItem(Cart cart)
        {
            return _repo.DeleteCartItem(cart);
        }

        public int ProcessOrder(int customerId)
        {
            Tuple<List<Cart>, decimal> cartItmes = GetAllCartItems(customerId);
            DateTime orderDate = DateTime.Now;
            decimal total = cartItmes.Item2;
            Order current = new Order
            {
                OrderDate = orderDate,
                Total = total,
                CustomerId = cartItmes.Item1[0].CustomerId,
                LocationId = cartItmes.Item1[0].LocationId
            };
            _repo.AddOrder(current);
            Order order = _repo.GetOrder(current);
            if (order == null) return -1;
            foreach(Cart c in cartItmes.Item1)
            {
                _repo.AddItem(
                    new Item 
                    {
                        Quantity = c.Quantity,
                        ProductId = c.ProductId,
                        OrderId = order.Id
                    }
                );
                Inventory stock = _repo.GetInventoryById(c.InventoryId);
                int newQuantity = stock.Quantity - c.Quantity;
                stock.Quantity = newQuantity;
                _repo.UpdateInventory(stock);
            }
            _repo.DeleteCart(customerId);
            return order.Id;
        }

        public List<Order> GetAllOrdersByCustomer(int customerId)
        {
            return _repo.GetAllOrdersByCustomer(customerId);
        }

        public List<Item> GetOrderItemByOrderId(int orderId)
        {
            return _repo.GetOrderItemByOrderId(orderId);
        }

        public List<Order> GetAllOrderByCustomer(int customerId, string sortingCode)
        {
            List<Order> result =  _repo.GetAllOrdersByCustomer(customerId);
            switch(sortingCode)
            {
                case "0" :
                    result.Sort(delegate(Order a, Order b) {
                        return a.Total.CompareTo(b.Total);
                    });
                    break;
                case "1" :
                    result.Sort(delegate(Order a, Order b) {
                        return b.Total.CompareTo(a.Total);
                    });
                    break;
                case "2" :
                    result.Sort(delegate(Order a, Order b) {
                        return a.OrderDate.CompareTo(b.OrderDate);
                    });
                    break;
                case "3" :
                    result.Sort(delegate(Order a, Order b) {
                        return b.OrderDate.CompareTo(a.OrderDate);
                    });
                    break;
            }
            return result;
        }
    }
}