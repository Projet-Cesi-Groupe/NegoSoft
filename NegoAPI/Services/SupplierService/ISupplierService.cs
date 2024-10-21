using NegoSoftShared.Models.Entities;
using NegoSoftShared.Models.ViewModels;

namespace NegoAPI.Services.SupplierService
{
    public interface ISupplierService
    {
        Task<IEnumerable<Supplier>> GetAllSuppliersAsync();
        Task<Supplier> GetSupplierByIdAsync(Guid id);
        Task<Supplier> CreateSupplierAsync(SupplierViewModel supplier);
        Task<Supplier> UpdateSupplierAsync(Guid id, SupplierViewModel supplier);
        Task<bool> DeleteSupplierAsync(Guid id);
        Task<IEnumerable<Product>> GetProductsBySupplierIdAsync(Guid id);
    }
}
