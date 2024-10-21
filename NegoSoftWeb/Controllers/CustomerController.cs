using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using NegoSoftShared.Models.Entities;
using NegoSoftWeb.Services.CustomerService;
using Microsoft.EntityFrameworkCore;
using NegoSoftWeb.Models.ViewModels;
using NegoSoftWeb.Models.Extensions;
using Microsoft.AspNetCore.Authorization;

namespace NegoSoftWeb.Controllers 
{ 
    public class CustomerController : Controller
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [Authorize]
        [HttpGet]
        // GET: Customers/Create
        public IActionResult Create()
        {
            return View();
        }


        // POST: Customers/Create
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CustomerViewModel customer)
        {
            if (customer == null)
            {
                   return BadRequest();
            }

            if (ModelState.IsValid)
            {
                _customerService.SaveCustomer(customer);
                return RedirectToAction("OrderAddress", "Address");
            }
            return View(customer);
        }
    }
}
