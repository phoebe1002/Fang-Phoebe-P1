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
        public LocationController(ILocationBL locationBL)
        {
            _locationBL = locationBL;
        }

        public ActionResult Index()
        {
            return View(_locationBL
            .GetAllLocations()
            .Select(location => new LocationVM(location))
            .ToList()
            );
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