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
            //show a list of products with inventories
            return View();
        }   


        public ActionResult HasInventory(int locationId)
        {
            ViewBag.Location = _locationBL.GetLocationById(locationId);

            return View(_inventoryBL
                .GetInventories(_locationBL.GetLocationById(locationId))
                // .Select(inventory => new InventoryVM(inventory))
                // .ToList()
                // );
                .Select(
                    inventory => new InventoryVM()
                    {
                        Quantity = inventory.Quantity,
                        Product = _productBL.GetProductById(inventory.ProductId)
                    })
                .ToList()
                //.ToDictionary(i => i.Product)
            );
        }


        public ActionResult Create(int locationId, int productId)
        {
            ViewBag.Location = _locationBL.GetLocationById(locationId);
            ViewBag.Product = _productBL.GetProductById(productId);
            return View(new InventoryVM(locationId, productId));
            //return View();
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
                    return RedirectToAction(nameof(Create), new { locationId = inventoryVM.LocationId,  productId = inventoryVM.ProductId } );
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