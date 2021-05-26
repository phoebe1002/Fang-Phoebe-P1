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
        public InventoryController(ILocationBL locationBL, IInventoryBL inventoryBL)
        {
            _locationBL = locationBL;
            _inventoryBL = inventoryBL;
        }

        public ActionResult Index()
        {
            return View();
        }   

        public ActionResult Create(int locationId, int productId)
        {
            //return View(new InventoryBL(locationId, productId));
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(InventoryVM inventoryVM)
        {
            try
            {
                // if (ModelState.IsValid)
                // {
                //     _inventoryBL.AddInventory(_locationBL.GetLocation
                        
                        
                //         new Inventory
                //         {
                //             Quantity = inventoryVM.Quantity
                //         }
                //     );
                //     return RedirectToAction(nameof(Index));
                // }
                return View();
            }
            catch
            {
                return View();
            }
        }
    }
}