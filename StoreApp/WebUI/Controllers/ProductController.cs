using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;
using System.Linq;
using StoreBL;
using StoreModels;
using WebUI.Models;
namespace WebUI.Controllers
{
    public class ProductController : Controller
    {
        private IProductBL _productBL;
        public ProductController(IProductBL productBL)
        {
            _productBL = productBL;
        }
        public ActionResult Index()
        {
            return View(_productBL
                .GetAllProducts()
                .Select(prod => new ProductVM(prod))
                .ToList()
            );
        }
        //GET
        public ActionResult Create()
        {
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
                    return RedirectToAction(nameof(Index));
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