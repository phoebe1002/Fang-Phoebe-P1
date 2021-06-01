using System.Collections.Generic;
using StoreModels;
using StoreDL;
using System;
using System.Linq;

namespace StoreBL
{
    public interface IOrderBL
    {
        Order AddOrder(Customer customer, Location location, Order order);
        Order AddOrder(Order order);
        Item AddItem(Order order, Product product, Item item);
        void AddToCart(Customer customer, Location location, Inventory inventory, Product product, Item item);
        Cart AddToCart(Cart cart);
        Cart GetCartItem(int id);
        Tuple<List<Cart>, decimal> GetAllCartItems(int customerId);
        Cart UpdateCartItem(Cart cart);
        Cart DeleteCartItem(Cart cart);
        int ProcessOrder(int customerId);
        List<Order> GetAllOrdersByCustomer(int customerId);
        List<Item> GetOrderItemByOrderId(int orderId);
        List<Order> GetAllOrderByCustomer(int customerId, string sortingCode);
    }
    
}