using Microsoft.EntityFrameworkCore;
using NegoSoftShared.Models.Entities;
using NegoSoftShared.Models.ViewModels;
using NegoSoftWeb.Data;


namespace NegoAPI.Services.CustomerOrderDetailsService
{
    public class CustomerOrderDetailsService : ICustomerOrderDetailsService
    {
        private readonly NegoSoftContext _context;

        public CustomerOrderDetailsService(NegoSoftContext context)
        {
            _context = context;
        }

        public async Task<CustomerOrderDetails> CreateCustomerOrderDetailsAsync(CustomerOrderDetailsViewModel customerOrderDetails)
        {
            if (customerOrderDetails == null)
            {
                return null;
            }

            var newCustomerOrderDetails = new CustomerOrderDetails
            {
                CodId = Guid.NewGuid(),
                CodQuantity = customerOrderDetails.CodQuantity,
                CodPrice = customerOrderDetails.CodPrice,
                CodProductId = customerOrderDetails.CodProductId,
                CodOrderId = customerOrderDetails.CodOrderId
            };
            _context.CustomerOrderDetails.Add(newCustomerOrderDetails);
            await _context.SaveChangesAsync();
            return newCustomerOrderDetails;
        }

        public async Task<CustomerOrderDetails> DeleteCustomerOrderDetailsAsync(Guid id)
        {
            var customerOrderDetails = await GetCustomerOrderDetailsByIdAsync(id);
            if (customerOrderDetails == null)
            {
                return null;
            }
            _context.CustomerOrderDetails.Remove(customerOrderDetails);
            await _context.SaveChangesAsync();
            return customerOrderDetails;
        }

        public async Task<CustomerOrderDetails> GetCustomerOrderDetailsByIdAsync(Guid id)
        {
            return await _context.CustomerOrderDetails.FindAsync(id);
        }

        public async Task<IEnumerable<CustomerOrderDetails>> GetAllCustomerOrderDetailsAsync()
        {
            return await _context.CustomerOrderDetails.ToListAsync();
        }

        public async Task<CustomerOrderDetails> UpdateCustomerOrderDetailsAsync(Guid id, CustomerOrderDetailsViewModel customerOrderDetails)
        {
            var existingCustomerOrderDetails = await _context.CustomerOrderDetails.FindAsync(id);
            if (existingCustomerOrderDetails == null)
            {
                return null;
            }
            try
            {
                existingCustomerOrderDetails.CodQuantity = customerOrderDetails.CodQuantity;
                existingCustomerOrderDetails.CodPrice = customerOrderDetails.CodPrice;
                existingCustomerOrderDetails.CodProductId = customerOrderDetails.CodProductId;
                existingCustomerOrderDetails.CodOrderId = customerOrderDetails.CodOrderId;
                await _context.SaveChangesAsync();
                return existingCustomerOrderDetails;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<CustomerOrderDetails>> GetCustomerOrderDetailsByCustomerOrderIdAsync(Guid id)
        {
            return await _context.CustomerOrderDetails
                .Where(cod => cod.CodOrderId == id)
                .Include(cod => cod.Product)
                .ToListAsync();
        }
    }
}
