using Microsoft.EntityFrameworkCore;
using NegoSoftShared.Models.Entities;
using NegoSoftWeb.Data;
using NegoSoftWeb.Models.Entities;
using NegoSoftWeb.Models.ViewModels;

namespace NegoAPI.Services.ProductService
{
    public class ProductService : IProductService
    {
        private readonly NegoSoftContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductService(NegoSoftContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _context.Products
                .ToListAsync();
        }

        public async Task<Product> GetProductByIdAsync(Guid id)
        {
            return await _context.Products
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ProId == id);
        }

        public async Task<bool> CreateProductAsync(Product product)
        {
            try
            {
                await _context.Products.AddAsync(product);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<Product> UpdateProductAsync(Product product)
        {
            _context.Entry(product).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<Product> DeleteProductAsync(Guid id)
        {
            var product = await GetProductByIdAsync(id);
            if (product == null) return null;
            try
            {
                if (product != null)
                {
                    _context.Products.Remove(product);
                    await _context.SaveChangesAsync();
                }
            }
            catch(DbUpdateException)
            {
                product.ProIsActive = false;
                await UpdateProductAsync(product);
            }
            return product;
        }

        public async Task<bool> ProductExistsAsync(Guid id)
        {
            return await _context.Products.AnyAsync(e => e.ProId == id);
        }

        

    }
}
