using NegoSoftWeb.Models.Entities;
using NegoSoftShared.Models.Entities;
using NegoSoftWeb.Models.ViewModels;

namespace NegoSoftWeb.Services.ProductService
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<Product> GetProductByIdAsync(Guid id);
        Task<Product> UpdateProductAsync(Product product);
        Task<bool> ProductExistsAsync(Guid id);
        Task<IEnumerable<int>> GetYearsAsync();
        Task<ProductSearchViewModel> SearchAsync(string searchString, Guid? typeId, Guid? supplierId, int? productYear, SortOrder sortOrder);
    }
}
