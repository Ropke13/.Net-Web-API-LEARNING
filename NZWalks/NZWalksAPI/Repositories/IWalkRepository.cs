using NZWalksAPI.Models.Domain;
using NZWalksAPI.Models.DTO;

namespace NZWalksAPI.Repositories
{
    public interface IWalkRepository
    {
        Task<List<Walk>> GetAllAsync();
        Task<Walk?> GetByIdAsync (Guid id);
        Task<Walk> CreateAsync(Walk data);
        Task<Walk?> UpdateAsync(Guid id, Walk data);
        Task<Walk?> DeleteAsync(Guid id);
    }
}
