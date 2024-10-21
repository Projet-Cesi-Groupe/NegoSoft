using NegoSoftShared.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using NegoAPI.Services.ProductService;
using NegoSoftWeb.Models.ViewModels;
using NegoSoftWeb.Models.Entities;
using NegoSoftShared.Models.ViewModels;

namespace NegoAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductController(IProductService productService, IWebHostEnvironment webHostEnvironment)
        {
            _productService = productService;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: api/Product
        [HttpGet]
        public async Task<IActionResult> GetAllProduct()
        {
            var products = await _productService.GetAllProductsAsync();

            if (products == null || !products.Any())
            {
                return NotFound("No products found.");
            }

            return Ok(products);
        }

        // GET: api/Product/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(Guid id)
        {
            var product = await _productService.GetProductByIdAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        // POST: api/Product
        [HttpPost]
        public async Task<ActionResult<Product>> CreateProduct([FromForm] ProductAddViewModel productViewModel, IFormFile image)
        {
            if (productViewModel == null || image == null)
            {
                return BadRequest("Invalid product data or missing image.");
            }

            // Save image to wwwroot
            string imagePath = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");
            if (!Directory.Exists(imagePath))
            {
                Directory.CreateDirectory(imagePath);
            }

            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
            string fullPath = Path.Combine(imagePath, fileName);

            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                await image.CopyToAsync(stream);
            }

            var product = new Product
            {
                ProId = productViewModel.ProId,
                ProName = productViewModel.ProName,
                ProDescription = productViewModel.ProDescription,
                ProSupplierId = productViewModel.ProSupplierId,
                ProPrice = productViewModel.ProPrice,
                ProBoxPrice = productViewModel.ProBoxPrice,
                ProTypeId = productViewModel.ProTypeId,
                ProStock = productViewModel.ProStock,
                ProYear = productViewModel.ProYear,
                ProAlcoholVolume = productViewModel.ProAlcoholVolume,
                ProIsActive = true,
                ProPictureName = fileName
            };

            var result = await _productService.CreateProductAsync(product);
            if (!result)
            {
                return StatusCode(500, "Failed to create product.");
            }

            return Ok("Product created successfully.");
        }

        // PUT: api/Product/5
        [HttpPut("{id}")]
        public async Task<IActionResult> EditProduct(Guid id, ProductEditViewModel product)
        {
            var OldProduct = await _productService.GetProductByIdAsync(id);

            if (OldProduct == null)
            {
                return NotFound();
            }

            try
            {
                OldProduct.ProId = id;
                OldProduct.ProName = product.ProName;
                OldProduct.ProDescription = product.ProDescription;
                OldProduct.ProPrice = product.ProPrice;
                OldProduct.ProStock = product.ProStock;
                OldProduct.ProTypeId = product.ProTypeId;
                OldProduct.ProSupplierId = product.ProSupplierId;
                OldProduct.ProBoxPrice = product.ProBoxPrice;
                OldProduct.ProYear = product.ProYear;
                OldProduct.ProAlcoholVolume = product.ProAlcoholVolume;
                OldProduct.ProIsActive = product.ProIsActive;

                await _productService.UpdateProductAsync(OldProduct);
            }
            catch (Exception)
            {
                if (!await _productService.ProductExistsAsync(OldProduct.ProId))
                {
                    return NotFound();
                }
                else
                {
                    return StatusCode(500);
                }
            }

            return NoContent();
        }

        // DELETE: api/Product/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            try
            {
                var response = await _productService.DeleteProductAsync(id);

                if (response == null)
                {
                    return NotFound("Produit introuvable.");
                }

                if (!response.ProIsActive)
                {
                    return Ok("Le produit a été désactivé car celui-ci est utilisé dans une commande.");
                }

                return Ok("Produit supprimé avec succès.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        // PUT: api/Product/stock/id
        [HttpPut("stock/{id}")]
        public async Task<IActionResult> UpdateProductStock(Guid id, [FromBody] int newQuantity)
        {
            var product = await _productService.GetProductByIdAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            try
            {
                product.ProStock += newQuantity;
                await _productService.UpdateProductAsync(product);
            }
            catch (Exception)
            {
                if (!await _productService.ProductExistsAsync(product.ProId))
                {
                    return NotFound();
                }
                else
                {
                    return StatusCode(500);
                }
            }

            return NoContent();
        }
    }
}
