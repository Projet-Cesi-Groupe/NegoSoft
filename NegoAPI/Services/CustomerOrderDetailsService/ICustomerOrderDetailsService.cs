using NegoSoftShared.Models.Entities;
using NegoSoftShared.Models.ViewModels;

namespace NegoAPI.Services.CustomerOrderDetailsService
{
    public interface ICustomerOrderDetailsService
    {
        Task<CustomerOrderDetails> CreateCustomerOrderDetailsAsync(CustomerOrderDetailsViewModel customerOrderDetails);
        Task<CustomerOrderDetails> UpdateCustomerOrderDetailsAsync(Guid id, CustomerOrderDetailsViewModel customerOrderDetails);
        Task<CustomerOrderDetails> DeleteCustomerOrderDetailsAsync(Guid id);
        Task<IEnumerable<CustomerOrderDetails>> GetAllCustomerOrderDetailsAsync();
        Task<CustomerOrderDetails> GetCustomerOrderDetailsByIdAsync(Guid id);
        Task<IEnumerable<CustomerOrderDetails>> GetCustomerOrderDetailsByCustomerOrderIdAsync(Guid id);
    }
}
