using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;
using System.Linq;
using StoreBL;
using StoreModels;
using WebUI.Models;
using System.Collections.Generic;

namespace WebUI.Controllers
{
    public class ProductController : Controller
    {
        private IProductBL _productBL;
        private ILocationBL _locationBL;
        public ProductController(IProductBL productBL, ILocationBL locationBL)
        {
            _productBL = productBL;
            _locationBL = locationBL;
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult UnclaimedList(int locationId)
        {
            ViewBag.Location = _locationBL.GetLocationById(locationId);

            return View(_productBL
            .GetAvailableProducts(_locationBL.GetLocationById(locationId))
            .Select(prod => new ProductVM(prod))
            .ToList()
            );
        }

        //GET
        public ActionResult Create(int locationId)
        {
            ViewBag.Location = _locationBL.GetLocationById(locationId);
            return View();
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProductVM productVM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _productBL.AddProduct(new Product
                    {
                        Name = productVM.Name,
                        Price = productVM.Price,
                        Description = productVM.Description
                    }
                    );
                    return RedirectToAction(nameof(UnclaimedList), new { locationId = productVM.LocationId });
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