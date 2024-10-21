using Microsoft.EntityFrameworkCore;
using NegoSoftShared.Models.Entities;
using NegoSoftShared.Models.ViewModels;
using NegoSoftWeb.Data;
using CustomerViewModel = NegoSoftShared.Models.ViewModels.CustomerViewModel;

namespace NegoAPI.Services.CustomerService
{
    public class CustomerService : ICustomerService
    {
        private readonly NegoSoftContext _context;
        public CustomerService(NegoSoftContext context)
        {
            _context = context;
        }
        public async Task<Customer> CreateCustomerAsync(CustomerViewModel customer)
        {
            if (customer == null)
            {
                return null;
            }

            var newCustomer = new Customer
            {
                CusId = Guid.NewGuid(),
                CusLastName = customer.CusLastName,
                CusFirstName = customer.CusFirstName,
                CusEmail = customer.CusEmail,
                CusPhone = customer.CusPhone,
                CusUserId = customer.CusUserId
            };
            _context.Customers.Add(newCustomer);
            await _context.SaveChangesAsync();
            return newCustomer;
        }
        public async Task<Customer> DeleteCustomerAsync(Guid id)
        {
            var customer = await GetCustomerByIdAsync(id);
            if (customer == null)
            {
                return null;
            }
            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
            return customer;
        }
        public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
        {
            return await _context.Customers.ToListAsync();
        }
        public async Task<Customer> GetCustomerByIdAsync(Guid id)
        {
            return await _context.Customers.FindAsync(id);
        }
        public async Task<Customer> UpdateCustomerAsync(Guid id, CustomerViewModel customer)
        {
            var existingCustomer = await _context.Customers.FindAsync(id);
            if (existingCustomer == null)
            {
                return null;
            }
            try
            {
                existingCustomer.CusLastName = customer.CusLastName;
                existingCustomer.CusFirstName = customer.CusFirstName;
                existingCustomer.CusEmail = customer.CusEmail;
                existingCustomer.CusPhone = customer.CusPhone;
                existingCustomer.CusUserId = customer.CusUserId;
                await _context.SaveChangesAsync();
                return existingCustomer;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }   
    }
}
