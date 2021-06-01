using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;
using System.Linq;
using StoreBL;
using StoreModels;
using WebUI.Models;

namespace WebUI.Controllers
{
    public class CustomerController : Controller
    {
        private ICustomerBL _customerBL;
        private ILocationBL _locationBL;
        public CustomerController(ICustomerBL customerBL, ILocationBL locationBL)
        {
            _customerBL = customerBL;
            _locationBL = locationBL;
        }
        public ActionResult Index()
        {
            // You have different kinds of Views:
            // Strongly typed views - tied to a model, you declare the model at the top of the page with
            //                          @model DataType
            // Weakly typed views - not tied to a model. You can still pass data to it via, viewbag
            //                      viewdata, tempdata etc
            // Dynamic views - pass a model, let view figure it out. @model dynamic
            // This is an example of a strongly typed view
            return View(_customerBL
                .GetAllCustomers()
                .Select(customer => new CustomerVM(customer))
                .ToList()
                );
        }

        public ActionResult Main(int customerId)
        {
            //ViewBag.Location = _locationBL.GetLocationById(1);
            ViewBag.Customer = _customerBL.GetCustomerById(customerId);
            return View();
        }

        public ActionResult Search()
        {
            return View();
        }

        // POST: CustomerController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Search(CustomerVM customerVM)
        {
            Customer customer = _customerBL.GetCustomerByPhone(customerVM.PhoneNumber);
            return RedirectToAction(nameof(Main), new {customerId = customer.Id});
        }

        // GET: CustomerController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CustomerController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CustomerVM customerVM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Customer current = new Customer
                    {
                        FirstName = customerVM.FirstName,
                        MiddleName = customerVM.MiddleName,
                        LastName = customerVM.LastName,
                        PhoneNumber = customerVM.PhoneNumber
                    };
                    _customerBL.AddCustomer(current);
                    Customer customer = _customerBL.GetCustomer(current);
                    return RedirectToAction(nameof(Main), new {customerId = customer.Id});
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