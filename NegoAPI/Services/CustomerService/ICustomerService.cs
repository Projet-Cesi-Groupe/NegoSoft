using NegoSoftShared.Models.Entities;
using NegoSoftShared.Models.ViewModels;
using NegoSoftWeb.Models.ViewModels;
using CustomerViewModel = NegoSoftShared.Models.ViewModels.CustomerViewModel;


namespace NegoAPI.Services.CustomerService
{
    public interface ICustomerService
    {
        Task<IEnumerable<Customer>> GetAllCustomersAsync();
        Task<Customer> GetCustomerByIdAsync(Guid id);
        Task<Customer> CreateCustomerAsync(CustomerViewModel type);
        Task<Customer> UpdateCustomerAsync(Guid id, CustomerViewModel type);
        Task<Customer> DeleteCustomerAsync(Guid id);
    }
}
