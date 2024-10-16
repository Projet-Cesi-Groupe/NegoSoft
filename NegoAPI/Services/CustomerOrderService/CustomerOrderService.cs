﻿using Microsoft.EntityFrameworkCore;
using NegoSoftShared.Models.Entities;
using NegoSoftShared.Models.ViewModels;
using NegoSoftWeb.Data;

namespace NegoAPI.Services.CustomerOrderService
{
    public class CustomerOrderService : ICustomerOrderService
    {
        private readonly NegoSoftContext _context;

        public CustomerOrderService(NegoSoftContext context)
        {
            _context = context;
        }

        public async Task<CustomerOrder> CreateCustomerOrderAsync(CustomerOrderViewModel customerOrder)
        {
            if (customerOrder == null)
            {
                return null;
            }

            var newCustomerOrder = new CustomerOrder
            {
                CoId = Guid.NewGuid(),
                CoDate = customerOrder.CoDate,
                CoTotal = customerOrder.CoTotal,
                CoCustomerId = customerOrder.CoCustomerId,
                CoAddressId = customerOrder.CoAddressId,
                CoState = customerOrder.CoState
            };
            _context.CustomerOrders.Add(newCustomerOrder);
            await _context.SaveChangesAsync();
            return newCustomerOrder;
        }

        public async Task<CustomerOrder> DeleteCustomerOrderAsync(Guid id)
        {
            var customerOrder = await GetCustomerOrderByIdAsync(id);
            if (customerOrder == null)
            {
                return null;
            }
            _context.CustomerOrders.Remove(customerOrder);
            await _context.SaveChangesAsync();
            return customerOrder;
        }

        public async Task<CustomerOrder> GetCustomerOrderByIdAsync(Guid id)
        {
            return await _context.CustomerOrders.FindAsync(id);
        }

        public async Task<IEnumerable<CustomerOrder>> GetAllCustomerOrdersAsync()
        {
            return await _context.CustomerOrders.ToListAsync();
        }

        public async Task<CustomerOrder> UpdateCustomerOrderAsync(Guid id, CustomerOrderViewModel customerOrder)
        {
            var existingCustomerOrder = await _context.CustomerOrders.FindAsync(id);
            if (existingCustomerOrder == null)
            {
                return null;
            }
            try
            {
                existingCustomerOrder.CoDate = customerOrder.CoDate;
                existingCustomerOrder.CoTotal = customerOrder.CoTotal;
                existingCustomerOrder.CoCustomerId = customerOrder.CoCustomerId;
                existingCustomerOrder.CoAddressId = customerOrder.CoAddressId;
                existingCustomerOrder.CoState = customerOrder.CoState;
                await _context.SaveChangesAsync();
                return existingCustomerOrder;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}