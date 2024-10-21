using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using NegoSoftShared.Models.Entities;
using NegoSoftShared.Models.ViewModels;
using NegoSoftWeb.Data;

namespace NegoAPI.Services.AddressService
{
    public class AddressService : IAddressService
    {
        private readonly NegoSoftContext _context;

        public AddressService(NegoSoftContext context)
        {
            _context = context;
        }

        public async Task<Address> CreateAddressAsync(AddressViewModel address)
        {
            if (address == null)
            {
                return null;
            }

            var addressExists = await AddressExists(address);

            // L'adresse existe déjà alors on la retourne
            if (addressExists != null)
            {
                return addressExists;
            }

            var newAddress = new Address
            {
                AddId = Guid.NewGuid(),
                AddDeliveryStreet = address.AddDeliveryStreet,
                AddDeliveryCity = address.AddDeliveryCity,
                AddDeliveryZipCode = address.AddDeliveryZipCode,
                AddDeliveryCountry = address.AddDeliveryCountry,
                AddBillingStreet = address.AddBillingStreet,
                AddBillingCity = address.AddBillingCity,
                AddBillingZipCode = address.AddBillingZipCode,
                AddBillingCountry = address.AddBillingCountry
            };

            await _context.Addresses.AddAsync(newAddress);
            await _context.SaveChangesAsync();
            return newAddress;
        }

        public async Task<Address> DeleteAddressAsync(Guid id)
        {
            var address = await GetAddressByIdAsync(id);
            if (address == null){
                return null;
            }
            _context.Addresses.Remove(address);
            await _context.SaveChangesAsync();
            return address;
        }

        public async Task<List<Address>> GetAllAddressesAsync()
        {
            return await _context.Addresses.ToListAsync();
        }

        public async Task<Address> GetAddressByIdAsync(Guid id)
        {
            return await _context.Addresses.FindAsync(id);
        }

        public async Task<Address> UpdateAddressAsync(Guid id, AddressViewModel address)
        {
            var addressToUpdate = await _context.Addresses.FindAsync(id);
            if (addressToUpdate == null){
                return null;
            }
            try
            {
                addressToUpdate.AddDeliveryStreet = address.AddDeliveryStreet;
                addressToUpdate.AddDeliveryCity = address.AddDeliveryCity;
                addressToUpdate.AddDeliveryZipCode = address.AddDeliveryZipCode;
                addressToUpdate.AddDeliveryCountry = address.AddDeliveryCountry;
                addressToUpdate.AddBillingStreet = address.AddBillingStreet;
                addressToUpdate.AddBillingCity = address.AddBillingCity;
                addressToUpdate.AddBillingZipCode = address.AddBillingZipCode;
                addressToUpdate.AddBillingCountry = address.AddBillingCountry;
                await _context.SaveChangesAsync();
                return addressToUpdate;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Address> AddressExists(AddressViewModel address)
        {
            return await _context.Addresses.FirstOrDefaultAsync(a => a.AddDeliveryStreet == address.AddDeliveryStreet &&
                                            a.AddDeliveryCity == address.AddDeliveryCity &&
                                            a.AddDeliveryZipCode == address.AddDeliveryZipCode &&
                                            a.AddDeliveryCountry == address.AddDeliveryCountry &&
                                            a.AddBillingStreet == address.AddBillingStreet &&
                                            a.AddBillingCity == address.AddBillingCity &&
                                            a.AddBillingZipCode == address.AddBillingZipCode &&
                                            a.AddBillingCountry == address.AddBillingCountry);
        }
    }
}
