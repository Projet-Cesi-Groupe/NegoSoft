using Microsoft.EntityFrameworkCore.Infrastructure;
using NegoSoftShared.Models.Entities;
using NegoSoftWeb.Models.ViewModels;

namespace NegoAPI.Services.ProductService
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<Product> GetProductByIdAsync(Guid id);
        Task<bool> CreateProductAsync(Product product);
        Task<Product> UpdateProductAsync(Product product);
        Task<Product> DeleteProductAsync(Guid id);
        Task<bool> ProductExistsAsync(Guid id);
    }
}
