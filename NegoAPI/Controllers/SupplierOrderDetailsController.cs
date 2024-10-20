using Microsoft.AspNetCore.Mvc;
using NegoAPI.Services.SupplierOrderDetailsService;
using NegoSoftShared.Models.Entities;
using NegoSoftShared.Models.ViewModels;

namespace NegoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupplierOrderDetailsController : ControllerBase
    {
        private readonly ISupplierOrderDetailsService _supplierOrderDetailsService;

        public SupplierOrderDetailsController(ISupplierOrderDetailsService supplierOrderDetailsService)
        {
            _supplierOrderDetailsService = supplierOrderDetailsService;
        }

        // GET: api/supplierorderdetails

        [HttpGet]
        public async Task<ActionResult<List<SupplierOrderDetails>>> GetSupplierOrdersDetails()
        {
            var supplierOrderDetails = await _supplierOrderDetailsService.GetAllSupplierOrderDetailsAsync();
            return Ok(supplierOrderDetails);
        }

        // GET: api/supplierorderdetails/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<SupplierOrder>> GetSupplierOrderDetail(Guid id)
        {
            var supplierOrderDetail = await _supplierOrderDetailsService.GetSupplierOrderDetailsByIdAsync(id);
            if (supplierOrderDetail == null) return NotFound();
            return Ok(supplierOrderDetail);
        }

        // PUT: api/supplierorderdetails/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSupplierOrderDetails(Guid id, SupplierOrderDetailsViewModel supplierOrderDetails)
        {
            if (supplierOrderDetails == null)
            {
                return BadRequest();
            }
            try
            {
                await _supplierOrderDetailsService.UpdateSupplierOrderDetailsAsync(id, supplierOrderDetails);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return NoContent();
        }

        // POST: api/supplierorderdetails
        [HttpPost]
        public async Task<ActionResult<SupplierOrderDetails>> CreateSupplierOrderDetails(SupplierOrderDetailsViewModel supplierOrderDetails)
        {
            if (supplierOrderDetails == null)
            {
                return BadRequest();
            }
            try
            {
                var newSupplierOrderDetails = await _supplierOrderDetailsService.CreateSupplierOrderDetailsAsync(supplierOrderDetails);
                return CreatedAtAction(nameof(GetSupplierOrderDetail), new { id = newSupplierOrderDetails.SodId }, newSupplierOrderDetails);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/supplierorderdetails/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSupplierOrderDetails(Guid id)
        {
            try
            {
                await _supplierOrderDetailsService.DeleteSupplierOrderDetailsAsync(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return NoContent();
        }

        // GET: api/supplierorderdetails/supplierorder/{id}
        [HttpGet("supplierorder/{id}")]
        public async Task<ActionResult<List<SupplierOrderDetails>>> GetSupplierOrderDetailsBySupplierOrderId(Guid id)
        {
            var supplierOrderDetails = await _supplierOrderDetailsService.GetSupplierOrderDetailsBySupplierOrderIdAsync(id);
            if (supplierOrderDetails == null) return NotFound();
            return Ok(supplierOrderDetails);
        }

    }
}
