using NZWalksAPI.Models.Domain;

namespace NZWalksAPI.Repositories
{
    public interface IWalkRepository
    {
        Task<List<Walk>> GetAllAsync(string? filterOn, string? filterQuery, string? sortBy, bool isAscending, int pageNumber = 1, int pageSize = 1000);
        Task<Walk?> GetByIdAsync (Guid id);
        Task<Walk> CreateAsync(Walk data);
        Task<Walk?> UpdateAsync(Guid id, Walk data);
        Task<Walk?> DeleteAsync(Guid id);
    }
}
