using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;
using System.Linq;
using StoreBL;
using StoreModels;
using WebUI.Models;

namespace WebUI.Controllers
{
    public class InventoryController : Controller
    {
        private IInventoryBL _inventoryBL;
        private ILocationBL _locationBL;
        private IProductBL _productBL;
        public InventoryController(ILocationBL locationBL, IProductBL productBL, IInventoryBL inventoryBL)
        {
            _locationBL = locationBL;
            _productBL = productBL;
            _inventoryBL = inventoryBL;
        }

        public ActionResult Index()
        {
            return View();
        }   
        
        public ActionResult ReplenishList(int locationId)
        {
            ViewBag.Location = _locationBL.GetLocationById(locationId);

            return View(_inventoryBL
                .GetInventories(_locationBL.GetLocationById(locationId))
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

        public ActionResult Create(int locationId, int productId)
        {
            ViewBag.Location = _locationBL.GetLocationById(locationId);
            ViewBag.Product = _productBL.GetProductById(productId);
            return View(new InventoryVM(locationId, productId));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(InventoryVM inventoryVM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _inventoryBL.AddInventory(
                        _locationBL.GetLocationById(inventoryVM.LocationId),
                        _productBL.GetProductById(inventoryVM.ProductId),
                        new Inventory
                        {
                            Quantity = inventoryVM.Quantity
                        }
                    );
                    return RedirectToAction(nameof(ReplenishList), new { locationId = inventoryVM.LocationId} );
                }
                return View();
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Edit(int id)
        {
            InventoryVM inventory = new InventoryVM(_inventoryBL.GetInventoryById(id));
            ViewBag.Location = _locationBL.GetLocationById(inventory.LocationId);
            ViewBag.Product = _productBL.GetProductById(inventory.ProductId);
            return View(inventory);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(InventoryVM inventoryVM)
        {
            try{
                if (ModelState.IsValid)
                {
                    _inventoryBL.UpdateInventory(new Inventory
                    {
                        Id = inventoryVM.Id,
                        Quantity = inventoryVM.Quantity,
                        LocationId = inventoryVM.LocationId,
                        ProductId = inventoryVM.ProductId
                    }

                    );
                    return RedirectToAction(nameof(ReplenishList), new { locationId = inventoryVM.LocationId} );
                }
                return View();
            }
            catch
            {
                return View();
            }
        }
    }
}