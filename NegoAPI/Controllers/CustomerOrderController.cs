using Microsoft.AspNetCore.Mvc;
using NegoAPI.Services.CustomerOrderService;
using NegoSoftShared.Models.Entities;
using NegoSoftShared.Models.ViewModels;

namespace NegoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerOrderController : ControllerBase
    {
        private readonly ICustomerOrderService _customerOrderService;

        public CustomerOrderController(ICustomerOrderService customerOrderService)
        {
            _customerOrderService = customerOrderService;
        }

        // GET: api/customerorder

        [HttpGet]
        public async Task<ActionResult<List<CustomerOrder>>> GetCustomerOrders()
        {
            var customerOrders = await _customerOrderService.GetAllCustomerOrdersAsync();
            return Ok(customerOrders);
        }

        // GET: api/customerorder/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerOrder>> GetCustomerOrder(Guid id)
        {
            var customerOrder = await _customerOrderService.GetCustomerOrderByIdAsync(id);
            if (customerOrder == null) return NotFound();
            return Ok(customerOrder);
        }

        // PUT: api/customerorder/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCustomerOrder(Guid id, CustomerOrderViewModel customerOrder)
        {
            if (customerOrder == null)
            {
                return BadRequest();
            }
            try
            {
                await _customerOrderService.UpdateCustomerOrderAsync(id, customerOrder);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return NoContent();
        }

        // POST: api/customerorder
        [HttpPost]
        public async Task<ActionResult<CustomerOrder>> CreateCustomerOrder(CustomerOrderViewModel customerOrder)
        {
            if (customerOrder == null)
            {
                return BadRequest();
            }
            try
            {
                var newCustomerOrder = await _customerOrderService.CreateCustomerOrderAsync(customerOrder);
                return CreatedAtAction(nameof(GetCustomerOrder), new { id = newCustomerOrder.CoId }, newCustomerOrder);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/customerorder/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomerOrder(Guid id)
        {
            try
            {
                await _customerOrderService.DeleteCustomerOrderAsync(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return NoContent();
        }

    }
}
