using Microsoft.EntityFrameworkCore;
using NegoSoftShared.Models.Entities;
using NegoSoftShared.Models.ViewModels;
using NegoSoftWeb.Data;

namespace NegoAPI.Services.SupplierOrderDetailsService
{
    public class SupplierOrderDetailsService : ISupplierOrderDetailsService
    {
        private readonly NegoSoftContext _context;

        public SupplierOrderDetailsService(NegoSoftContext context)
        {
            _context = context;
        }

        public async Task<SupplierOrderDetails> CreateSupplierOrderDetailsAsync(SupplierOrderDetailsViewModel supplierOrderDetails)
        {
            if (supplierOrderDetails == null)
            {
                return null;
            }

            var newSupplierOrderDetails = new SupplierOrderDetails
            {
                SodId = Guid.NewGuid(),
                SodQuantity = supplierOrderDetails.SodQuantity,
                SodPrice = supplierOrderDetails.SodPrice,
                SodProductId = supplierOrderDetails.SodProductId,
                SodOrderId = supplierOrderDetails.SodOrderId
            };
            _context.SupplierOrderDetails.Add(newSupplierOrderDetails);
            await _context.SaveChangesAsync();
            return newSupplierOrderDetails;
        }

        public async Task<SupplierOrderDetails> DeleteSupplierOrderDetailsAsync(Guid id)
        {
            var supplierOrderDetails = await GetSupplierOrderDetailsByIdAsync(id);
            if (supplierOrderDetails == null)
            {
                return null;
            }
            _context.SupplierOrderDetails.Remove(supplierOrderDetails);
            await _context.SaveChangesAsync();
            return supplierOrderDetails;
        }
        
        public async Task<SupplierOrderDetails> GetSupplierOrderDetailsByIdAsync(Guid id)
        {
            return await _context.SupplierOrderDetails
                .Include(sod => sod.Product)
                .FirstOrDefaultAsync(sod => sod.SodId == id);
        }

        public async Task<IEnumerable<SupplierOrderDetails>> GetAllSupplierOrderDetailsAsync()
        {
            return await _context.SupplierOrderDetails.ToListAsync();
        }

        public async Task<SupplierOrderDetails> UpdateSupplierOrderDetailsAsync(Guid id, SupplierOrderDetailsViewModel supplierOrderDetails)
        {
            var existingSupplierOrderDetails = await _context.SupplierOrderDetails.FindAsync(id);
            if (existingSupplierOrderDetails == null)
            {
                return null;
            }
            try
            {
                existingSupplierOrderDetails.SodQuantity = supplierOrderDetails.SodQuantity;
                existingSupplierOrderDetails.SodPrice = supplierOrderDetails.SodPrice;
                existingSupplierOrderDetails.SodProductId = supplierOrderDetails.SodProductId;
                existingSupplierOrderDetails.SodOrderId = supplierOrderDetails.SodOrderId;
                await _context.SaveChangesAsync();
                return existingSupplierOrderDetails;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<SupplierOrderDetails>> GetSupplierOrderDetailsBySupplierOrderIdAsync(Guid id)
        {
            return await _context.SupplierOrderDetails
                .Where(sod => sod.SodOrderId == id)
                .Include(sod => sod.Product)
                .ToListAsync();
        }
    }
}
