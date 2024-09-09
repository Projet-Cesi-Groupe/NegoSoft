﻿using Microsoft.AspNetCore.Mvc;
using NegoSoftWeb.Models.ViewModels;
using NegoSoftWeb.Services.AddressService;

namespace NegoSoftWeb.Controllers
{
    public class AddressController : Controller
    {
        private readonly IAddressService _addressService;

        public AddressController(IAddressService addressService)
        {
            _addressService = addressService;
        }

        // GET: Address/Details/{AddId}
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var address = await _addressService.GetAddressByIdAsync(id.Value);
            if (address == null)
            {
                return NotFound();
            }

            return View(address);
        }

        // GET: Address/OrderAddress
        public IActionResult OrderAddress()
        {
            return View();
        }

        // POST: Address/OrderAddress
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OrderAddress(AddressViewModel address)
        {
            if (address == null)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                _addressService.SaveAddress(address);
                return View(address);
            }
            return View(address);
        }
    }
}