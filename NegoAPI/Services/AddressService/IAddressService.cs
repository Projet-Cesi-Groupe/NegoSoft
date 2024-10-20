using NegoSoftShared.Models.Entities;
using NegoSoftShared.Models.ViewModels;

namespace NegoAPI.Services.AddressService
{
    public interface IAddressService
    {
        Task<Address> CreateAddressAsync(AddressViewModel address);
        Task<Address> DeleteAddressAsync(Guid id);
        Task<List<Address>> GetAllAddressesAsync();
        Task<Address> GetAddressByIdAsync(Guid id);
        Task<Address> UpdateAddressAsync(Guid id, AddressViewModel address);
        Task<Address> AddressExists(AddressViewModel address);
    }
}
