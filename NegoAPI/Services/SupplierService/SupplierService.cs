using Microsoft.EntityFrameworkCore;
using NegoSoftShared.Models.Entities;
using NegoSoftShared.Models.ViewModels;
using NegoSoftWeb.Data;
using System.Data.OleDb;

namespace NegoAPI.Services.SupplierService
{
    public class SupplierService : ISupplierService
    {
        private readonly NegoSoftContext _context;

        public SupplierService(NegoSoftContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Supplier>> GetAllSuppliersAsync()
        {
            return await _context.Suppliers.ToListAsync();
        }

        public async Task<Supplier> GetSupplierByIdAsync(Guid id)
        {
            return await _context.Suppliers.FindAsync(id);
        }

        public async Task<Supplier> CreateSupplierAsync(SupplierViewModel supplier)
        {
            if (supplier == null) 
            { 
                return null;
            }

            var newsupplier = new Supplier
            {
                SupId = Guid.NewGuid(),
                SupName = supplier.SupName,
                SupDefaultAddressId = supplier.SupDefaultAddressId,
                SupPhone = supplier.SupPhone,
                SupEmail = supplier.SupEmail
            };
            _context.Suppliers.Add(newsupplier);
            await _context.SaveChangesAsync();
            return newsupplier;
        }

        public async Task<Supplier> UpdateSupplierAsync(Guid id, SupplierViewModel supplier)
        {
            if (supplier == null)
            {
                return null;
            }

            var NewSupplier = new Supplier
            {
                SupId = id,
                SupName = supplier.SupName,
                SupDefaultAddressId = supplier.SupDefaultAddressId,
                SupPhone = supplier.SupPhone,
                SupEmail = supplier.SupEmail
            };

            _context.Entry(NewSupplier).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NewSupplier;
        }

        public async Task<bool> DeleteSupplierAsync(Guid id)
        {
            var supplier = await GetSupplierByIdAsync(id);
            if (supplier == null) return false;

            _context.Suppliers.Remove(supplier);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Product>> GetProductsBySupplierIdAsync(Guid id)
        {
            return await _context.Products.Where(p => p.ProSupplierId == id).ToListAsync();
        }

    }
}
