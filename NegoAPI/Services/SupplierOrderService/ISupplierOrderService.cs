using NegoSoftShared.Models.Entities;
using NegoSoftShared.Models.ViewModels;

namespace NegoAPI.Services.SupplierOrderService
{
    public interface ISupplierOrderService
    {
        public Task<SupplierOrder> CreateSupplierOrderAsync(SupplierOrderViewModel supplierOrder);
        public Task<SupplierOrder> UpdateSupplierOrderAsync(Guid id, SupplierOrderViewModel supplierOrder);
        public Task<SupplierOrder> DeleteSupplierOrderAsync(Guid id);
        public Task<IEnumerable<SupplierOrder>> GetAllSupplierOrdersAsync();
        public Task<SupplierOrder> GetSupplierOrderByIdAsync(Guid id);
    }
}
