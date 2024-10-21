using Microsoft.AspNetCore.Mvc;
using NegoAPI.Services.SupplierOrderService;
using NegoSoftShared.Models.Entities;
using NegoSoftShared.Models.ViewModels;

namespace NegoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupplierOrderController : ControllerBase
    {
        private readonly ISupplierOrderService _supplierOrderService;

        public SupplierOrderController(ISupplierOrderService supplierOrderService)
        {
            _supplierOrderService = supplierOrderService;
        }

        // GET: api/supplierorder

        [HttpGet]
        public async Task<ActionResult<List<SupplierOrder>>> GetSupplierOrders()
        {
            var supplierOrders = await _supplierOrderService.GetAllSupplierOrdersAsync();
            return Ok(supplierOrders);
        }

        // GET: api/supplierorder/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<SupplierOrder>> GetSupplierOrder(Guid id)
        {
            var supplierOrder = await _supplierOrderService.GetSupplierOrderByIdAsync(id);
            if (supplierOrder == null) return NotFound();
            return Ok(supplierOrder);
        }

        // PUT: api/supplierorder/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSupplierOrder(Guid id, SupplierOrderViewModel supplierOrder)
        {
            if (supplierOrder == null)
            {
                return BadRequest();
            }
            try
            {
                await _supplierOrderService.UpdateSupplierOrderAsync(id, supplierOrder);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return NoContent();
        }

        // POST: api/supplierorder
        [HttpPost]
        public async Task<ActionResult<SupplierOrder>> CreateSupplierOrder(SupplierOrderViewModel supplierOrder)
        {
            if (supplierOrder == null)
            {
                return BadRequest();
            }
            try
            {
                var newSupplierOrder = await _supplierOrderService.CreateSupplierOrderAsync(supplierOrder);
                return CreatedAtAction(nameof(GetSupplierOrder), new { id = newSupplierOrder.SoId }, newSupplierOrder);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/supplierorder/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSupplierOrder(Guid id)
        {
            try
            {
                await _supplierOrderService.DeleteSupplierOrderAsync(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return NoContent();
        }
    }
}
