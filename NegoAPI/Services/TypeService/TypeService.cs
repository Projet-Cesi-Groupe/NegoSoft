using Microsoft.EntityFrameworkCore;
using NegoSoftShared.Models.Entities;
using NegoSoftShared.Models.ViewModels;
using NegoSoftWeb.Data;
using Type = NegoSoftShared.Models.Entities.Type;

namespace NegoAPI.Services.TypeService
{
    public class TypeService : ITypeService
    {
        private readonly NegoSoftContext _context;

        public TypeService(NegoSoftContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Type>> GetAllTypesAsync()
        {
            return await _context.Types.ToListAsync();
        }

        public async Task<Type> GetTypeByIdAsync(Guid id)
        {
            return await _context.Types.FindAsync(id);
        }

        public async Task<Type> CreateTypeAsync(TypeViewModel type)
        {
            if (type == null)
            {
                return null;
            }
            try
            {
                var newType = new Type
                {
                    TypId = Guid.NewGuid(),
                    TypLibelle = type.TypLibelle
                };
                _context.Types.Add(newType);
                await _context.SaveChangesAsync();
                return newType;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Type> UpdateTypeAsync(Guid id, TypeViewModel type)
        {
            if (type == null)
            {
                return null;
            }

            var newType = new Type
            {
                TypId = id,
                TypLibelle = type.TypLibelle
            };

            _context.Entry(newType).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return newType;
        }

        public async Task<Type> DeleteTypeAsync(Guid id)
        {
            var type = await GetTypeByIdAsync(id);
            if (type != null)
            {
                _context.Types.Remove(type);
                await _context.SaveChangesAsync();
            }
            return type;
        }
    }
}
