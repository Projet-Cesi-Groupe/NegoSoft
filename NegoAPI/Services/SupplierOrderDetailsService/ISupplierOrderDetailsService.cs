using NegoSoftShared.Models.Entities;
using NegoSoftShared.Models.ViewModels;

namespace NegoAPI.Services.SupplierOrderDetailsService
{
    public interface ISupplierOrderDetailsService
    {
        Task<SupplierOrderDetails> CreateSupplierOrderDetailsAsync(SupplierOrderDetailsViewModel supplierOrderDetails);
        Task<SupplierOrderDetails> UpdateSupplierOrderDetailsAsync(Guid id, SupplierOrderDetailsViewModel supplierOrderDetails);
        Task<SupplierOrderDetails> DeleteSupplierOrderDetailsAsync(Guid id);
        Task<IEnumerable<SupplierOrderDetails>> GetAllSupplierOrderDetailsAsync();
        Task<SupplierOrderDetails> GetSupplierOrderDetailsByIdAsync(Guid id);
        Task<IEnumerable<SupplierOrderDetails>> GetSupplierOrderDetailsBySupplierOrderIdAsync(Guid supplierOrderId);
    }
}
