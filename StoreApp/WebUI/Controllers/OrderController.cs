using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;
using System.Linq;
using StoreBL;
using StoreModels;
using WebUI.Models;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace WebUI.Controllers
{
    public class OrderController : Controller
    {
        private IProductBL _productBL;
        private ILocationBL _locationBL;
        private ICustomerBL _customerBL;
        private IInventoryBL _inventoryBL;
        private IOrderBL _orderBL;

        public OrderController(IProductBL productBL, ILocationBL locationBL, ICustomerBL customerBL, IInventoryBL inventoryBL, IOrderBL orderBL)
        {
            _productBL = productBL;
            _locationBL = locationBL;
            _customerBL = customerBL;
            _inventoryBL = inventoryBL;
            _orderBL = orderBL;
        }

        public ActionResult SelectProduct(int locationId, int customerId)
        {
            ViewBag.Location = _locationBL.GetLocationById(locationId);
            ViewBag.Customer = _customerBL.GetCustomerById(customerId);
            return View(_inventoryBL
                .GetUnselectedProducts(_locationBL.GetLocationById(locationId), customerId)
                .Select(
                    inventory => new InventoryVM()
                    {
                        Id = inventory.Id,
                        Quantity = inventory.Quantity,
                        Product = _productBL.GetProductById(inventory.ProductId)
                    })
                .ToList()
            );
        }

        public ActionResult Start(int locationId, int customerId)
        {
            ViewBag.Location = _locationBL.GetLocationById(locationId);
            ViewBag.Customer = _customerBL.GetCustomerById(customerId);
            ViewBag.Stock = _inventoryBL
                .GetAvaliableProductsByInventory(_locationBL.GetLocationById(locationId))
                .Select(
                    inventory => new InventoryVM()
                    {
                        Id = inventory.Id,
                        Quantity = inventory.Quantity,
                        Product = _productBL.GetProductById(inventory.ProductId)
                    })
                .ToList();
            
            return View();
        }

        public ActionResult AddToCart(int customerId, int locationId, int inventoryId, int productId, int id)
        {
            ViewBag.Customer = _customerBL.GetCustomerById(customerId);
            ViewBag.Location = _locationBL.GetLocationById(locationId);
            ViewBag.Inventory = _inventoryBL.GetInventoryById(inventoryId);
            ViewBag.Product = _productBL.GetProductById(productId);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddToCart(CartVM cart)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _orderBL.AddToCart(
                        new Cart
                        {
                            LocationId = cart.LocationId,
                            CustomerId = cart.CustomerId,
                            InventoryId = cart.InventoryId,
                            ProductId = cart.ProductId,
                            Quantity = cart.Quantity,
                            Price = cart.Price,
                            Name = cart.Name
                        }
                    );
                    return RedirectToAction(nameof(SelectProduct), new {locationId = cart.LocationId, customerId = cart.CustomerId});
                }
                return View();
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Checkout(int locationId, int customerId)
        {
            ViewBag.Customer = _customerBL.GetCustomerById(customerId);
            ViewBag.Location = _locationBL.GetLocationById(locationId);

            Tuple<List<Cart>, decimal> result = _orderBL.GetAllCartItems(customerId);
            if (result.Item1.Count > 0 )
            {
                ViewData.Add("Total", result.Item2);
            }
            else
            {
                ViewData.Add("Total", "No item in the cart yet");
            }
            return View(result.Item1
                .Select(
                    cart => new CartVM
                        {
                            Id = cart.Id,
                            LocationId = cart.LocationId,
                            CustomerId = cart.CustomerId,
                            InventoryId = cart.InventoryId,
                            ProductId = cart.ProductId,
                            Quantity = cart.Quantity,
                            Price = cart.Price,
                            Name = cart.Name,
                            Total = cart.Quantity * cart.Price
                        })
                .ToList()
            );
        }

        public ActionResult Main(int customerId, int locationId)
        {
            ViewBag.Customer = _customerBL.GetCustomerById(customerId);
            ViewBag.Location = _locationBL.GetLocationById(locationId);
            Tuple<List<Cart>, decimal> result = _orderBL.GetAllCartItems(customerId);
            int proceed = _orderBL.ProcessOrder(customerId);
            if (result.Item1.Count < 0 )
            {
                ViewData.Add("Message", "Your cart is empty");
            }
            else if (proceed != -1)
            {
                ViewData.Add("Message", "Your order has been placed!");
            }
            return View();
        }

        public ActionResult Edit(int id)
        {
            CartVM item = new CartVM(_orderBL.GetCartItem(id));
            ViewBag.Customer = _customerBL.GetCustomerById(item.CustomerId);
            ViewBag.Location = _locationBL.GetLocationById(item.LocationId);
            ViewBag.Inventory = _inventoryBL.GetInventoryById(item.InventoryId);
            ViewBag.Product = _productBL.GetProductById(item.ProductId);
            return View(item);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CartVM cart)
        {
            try{
                if (ModelState.IsValid)
                {
                    _orderBL.UpdateCartItem(                        
                        new Cart
                        {
                            Id = cart.Id,
                            LocationId = cart.LocationId,
                            CustomerId = cart.CustomerId,
                            InventoryId = cart.InventoryId,
                            ProductId = cart.ProductId,
                            Quantity = cart.Quantity,
                            Price = cart.Price,
                            Name = cart.Name
                        }
                    );
                    return RedirectToAction(nameof(Checkout), new {locationId = cart.LocationId, customerId = cart.CustomerId});
                }
                return View();
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Create(int customerId, int locationId)
        {
            ViewBag.Customer = _customerBL.GetCustomerById(customerId);
            ViewBag.Location = _locationBL.GetLocationById(locationId);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(OrderVM orderVM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int total = 4;
                    DateTime orderDate = DateTime.Now;
                    _orderBL.AddOrder(new Order{
                        Id = orderVM.Id,
                        OrderDate = orderDate,
                        Total = total,
                        CustomerId = orderVM.CustomerId,
                        LocationId = orderVM.LocationId
                    }
                    );
                    return RedirectToAction(nameof(SelectProduct), new {locationId = orderVM.LocationId, customerId = orderVM.CustomerId});
                }
                return View();
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Delete(int id)
        {
            CartVM item = new CartVM(_orderBL.GetCartItem(id));
            ViewBag.Customer = _customerBL.GetCustomerById(item.CustomerId);
            ViewBag.Location = _locationBL.GetLocationById(item.LocationId);
            ViewBag.Inventory = _inventoryBL.GetInventoryById(item.InventoryId);
            ViewBag.Product = _productBL.GetProductById(item.ProductId);
            return View(item);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {{}
            try
            {
                Cart cart = _orderBL.DeleteCartItem(_orderBL.GetCartItem(id));
                return RedirectToAction(nameof(Checkout), new {locationId = cart.LocationId, customerId = cart.CustomerId});
            }
            catch
            {
                return View();
            }
        }
    }
}