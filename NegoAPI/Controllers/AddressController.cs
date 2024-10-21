using Microsoft.AspNetCore.Mvc;
using NegoAPI.Services.AddressService;
using NegoSoftShared.Models.Entities;
using NegoSoftShared.Models.ViewModels;

namespace NegoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IAddressService _addressService;

        public AddressController(IAddressService addressService)
        {
            _addressService = addressService;
        }

        // GET: api/address
        [HttpGet]
        public async Task<ActionResult<List<Address>>> GetAddressProducts()
        {
            var addresses = await _addressService.GetAllAddressesAsync();
            return Ok(addresses);
        }

        // GET: api/address/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Address>> GetAddress(Guid id)
        {
            var address = await _addressService.GetAddressByIdAsync(id);
            if (address == null) return NotFound();
            return Ok(address);
        }

        // PUT: api/address/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAddress(Guid id, AddressViewModel address)
        {
            if (address == null)
            {
                return BadRequest();
            }
            try
            {
                await _addressService.UpdateAddressAsync(id, address);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return NoContent();
        }

        // POST: api/address
        [HttpPost]
        public async Task<ActionResult<Address>> CreateAddress(AddressViewModel address)
        {
            var createdAddress = await _addressService.CreateAddressAsync(address);
            return CreatedAtAction(nameof(GetAddress), new { id = createdAddress.AddId }, createdAddress);
        }

        // DELETE: api/address/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAddress(Guid id)
        {
            try
            {
                await _addressService.DeleteAddressAsync(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return NoContent();
        }
    }
}
