using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;
using System.Linq;
using StoreBL;
using StoreModels;
using WebUI.Models;

namespace WebUI.Controllers
{
    
    public class LocationController : Controller
    {
        private ILocationBL _locationBL;
        private ICustomerBL _customerBL;
        public LocationController(ILocationBL locationBL, ICustomerBL customerBL)
        {
            _locationBL = locationBL;
            _customerBL = customerBL;
        }

        public ActionResult Index()
        {
            return View(_locationBL
            .GetAllLocations()
            .Select(location => new LocationVM(location))
            .ToList()
            );
        }
        
        public ActionResult SelectLocation(int customerId)
        {
            ViewBag.Customer = _customerBL.GetCustomerById(customerId);
            return View(_locationBL
            .GetAllLocations()
            .Select(location => new LocationVM(location))
            .ToList()
            );
        }
        
        public ActionResult Main(int locationId)
        {
            ViewBag.Location = _locationBL.GetLocationById(locationId);
            return View();
        }

        //GET
        public ActionResult Create()
        {
            return View();
        }
        
        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(LocationVM locationVM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _locationBL.AddLocation(new Location
                    {
                        Name = locationVM.Name,
                        Address = locationVM.Address
                    });
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