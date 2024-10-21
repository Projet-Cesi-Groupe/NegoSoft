using NegoSoftShared.Models.Entities;
using NegoSoftShared.Models.ViewModels;
using Type = NegoSoftShared.Models.Entities.Type;

namespace NegoAPI.Services.TypeService
{
    public interface ITypeService
    {
        Task<IEnumerable<Type>> GetAllTypesAsync();
        Task<Type> GetTypeByIdAsync(Guid id);
        Task<Type> CreateTypeAsync(TypeViewModel type);
        Task<Type> UpdateTypeAsync(Guid id, TypeViewModel type);
        Task<Type> DeleteTypeAsync(Guid id);
    }
}
