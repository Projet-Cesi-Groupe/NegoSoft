using Microsoft.AspNetCore.Mvc;
using NegoAPI.Services.CustomerOrderDetailsService;
using NegoSoftShared.Models.Entities;
using NegoSoftShared.Models.ViewModels;

namespace NegoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerOrderDetailsController : ControllerBase
    {
        private readonly ICustomerOrderDetailsService _customerOrderDetailsService;

        public CustomerOrderDetailsController(ICustomerOrderDetailsService customerOrderDetailsService)
        {
            _customerOrderDetailsService = customerOrderDetailsService;
        }

        // GET: api/customerorderdetails

        [HttpGet]
        public async Task<ActionResult<List<CustomerOrderDetails>>> GetAllCustomerOrderDetails()
        {
            var customerOrdersDetails = await _customerOrderDetailsService.GetAllCustomerOrderDetailsAsync();
            return Ok(customerOrdersDetails);
        }

        // GET: api/customerorderdetails/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerOrderDetails>> GetCustomerOrderDetail(Guid id)
        {
            var customerOrderDetail = await _customerOrderDetailsService.GetCustomerOrderDetailsByIdAsync(id);
            if (customerOrderDetail == null) return NotFound();
            return Ok(customerOrderDetail);
        }

        // GET: api/customerorderdetails/customerorder/{id}
        [HttpGet("customerorder/{id}")]
        public async Task<ActionResult<List<CustomerOrderDetails>>> GetCustomerOrderDetailsByCustomerOrderId(Guid id)
        {
            var customerOrderDetails = await _customerOrderDetailsService.GetCustomerOrderDetailsByCustomerOrderIdAsync(id);
            if (customerOrderDetails == null) return NotFound();
            return Ok(customerOrderDetails);
        }

        // PUT: api/customerorderdetails/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCustomerOrderDetails(Guid id, CustomerOrderDetailsViewModel customerOrderDetails)
        {
            if (customerOrderDetails == null)
            {
                return BadRequest();
            }
            try
            {
                await _customerOrderDetailsService.UpdateCustomerOrderDetailsAsync(id, customerOrderDetails);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return NoContent();
        }

        // POST: api/customerorderdetails
        [HttpPost]
        public async Task<ActionResult<CustomerOrderDetails>> CreateCustomerOrderDetails(CustomerOrderDetailsViewModel customerOrderDetails)
        {
            if (customerOrderDetails == null)
            {
                return BadRequest();
            }
            try
            {
                var newCustomerOrderDetail = await _customerOrderDetailsService.CreateCustomerOrderDetailsAsync(customerOrderDetails);
                return CreatedAtAction(nameof(GetCustomerOrderDetail), new { id = newCustomerOrderDetail.CodId }, newCustomerOrderDetail);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/customerorderdetails/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomerOrderDetails(Guid id)
        {
            try
            {
                await _customerOrderDetailsService.DeleteCustomerOrderDetailsAsync(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return NoContent();
        }
    }
}
