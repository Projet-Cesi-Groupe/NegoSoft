using NegoSoftShared.Models.Entities;
using NegoSoftShared.Models.ViewModels;

namespace NegoAPI.Services.CustomerOrderService
{
    public interface ICustomerOrderService
    {
        Task<CustomerOrder> CreateCustomerOrderAsync(CustomerOrderViewModel customerOrder);
        Task<CustomerOrder> UpdateCustomerOrderAsync(Guid id, CustomerOrderViewModel customerOrder);
        Task<CustomerOrder> DeleteCustomerOrderAsync(Guid id);
        Task<IEnumerable<CustomerOrder>> GetAllCustomerOrdersAsync();
        Task<CustomerOrder> GetCustomerOrderByIdAsync(Guid id);
    }
}
