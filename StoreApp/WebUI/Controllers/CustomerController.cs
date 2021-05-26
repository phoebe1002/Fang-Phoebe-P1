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
        public CustomerController(ICustomerBL customerBL)
        {
            _customerBL = customerBL;
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
                    _customerBL.AddCustomer(new Customer
                    {
                        FirstName = customerVM.FirstName,
                        MiddleName = customerVM.MiddleName,
                        LastName = customerVM.LastName,
                        PhoneNumber = customerVM.PhoneNumber
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