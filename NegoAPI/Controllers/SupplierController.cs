using Microsoft.AspNetCore.Mvc;
using NegoAPI.Services.SupplierService;
using NegoSoftShared.Models.Entities;
using NegoSoftShared.Models.ViewModels;

namespace NegoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupplierController : ControllerBase
    {
        private readonly ISupplierService _supplierService;

        public SupplierController(ISupplierService supplierService)
        {
            _supplierService = supplierService;
        }

        // GET: api/supplier
        [HttpGet]
        public async Task<ActionResult<List<Supplier>>> GetSuppliers()
        {
            var suppliers = await _supplierService.GetAllSuppliersAsync();
            return Ok(suppliers);
        }

        // GET: api/supplier/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Supplier>> GetSupplier(Guid id)
        {
            var supplier = await _supplierService.GetSupplierByIdAsync(id);
            if (supplier == null) return NotFound();
            return Ok(supplier);
        }

        // POST: api/supplier
        [HttpPost]
        public async Task<ActionResult<Supplier>> CreateSupplier(SupplierViewModel supplier)
        {
            var createdSupplier = await _supplierService.CreateSupplierAsync(supplier);
            return CreatedAtAction(nameof(GetSupplier), new { id = createdSupplier.SupId }, createdSupplier);
        }

        // PUT: api/supplier/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSupplier(Guid id, SupplierViewModel supplier)
        {
            if (supplier == null)
            {
                return BadRequest();
            }
            try 
            { 
                await _supplierService.UpdateSupplierAsync(id, supplier);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return NoContent();
        }

        // DELETE: api/supplier/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSupplier(Guid id)
        {
            try
            {
                await _supplierService.DeleteSupplierAsync(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return NoContent();
        }

        // GET: api/supplier/{id}/products
        [HttpGet("{id}/products")]
        public async Task<ActionResult<List<Product>>> GetProductsBySupplierId(Guid id)
        {
            var products = await _supplierService.GetProductsBySupplierIdAsync(id);
            return Ok(products);
        }
    }
   
}
