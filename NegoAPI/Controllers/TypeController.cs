using Microsoft.AspNetCore.Mvc;
using NegoSoftShared.Models.ViewModels;
using NegoSoftShared.Models.Entities;
using Type = NegoSoftShared.Models.Entities.Type;
using NegoAPI.Services.TypeService;

namespace NegoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TypeController : ControllerBase
    {

        private readonly ITypeService _typeService;

        public TypeController(ITypeService typeService)
        {
            _typeService = typeService;
        }


        // GET: api/type
        [HttpGet]
        public async Task<ActionResult<List<Type>>> GetTypes()
        {
            var types = await _typeService.GetAllTypesAsync();
            return Ok(types);
        }

        // GET: api/type/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Type>> GetType(Guid id)
        {
            var type = await _typeService.GetTypeByIdAsync(id);
            if (type == null) return NotFound();
            return Ok(type);
        }

        // POST: api/type
        [HttpPost]
        public async Task<ActionResult<Type>> CreateType(TypeViewModel type)
        {
            var createdType = await _typeService.CreateTypeAsync(type);
            return CreatedAtAction(nameof(GetType), new { id = createdType.TypId }, createdType);
        }

        // PUT: api/type/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateType(Guid id, TypeViewModel type)
        {
            if (type == null)
            {
                return BadRequest();
            }
            try
            {
                await _typeService.UpdateTypeAsync(id, type);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return NoContent();
        }

        // DELETE: api/type/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteType(Guid id)
        {
            try
            {
                await _typeService.DeleteTypeAsync(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return NoContent();
        }
    }
}
