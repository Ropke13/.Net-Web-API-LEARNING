using Microsoft.EntityFrameworkCore;
using NZWalksAPI.Data;
using NZWalksAPI.Models.Domain;
using NZWalksAPI.Repositories;

namespace NZWalksAPI.Services
{
    public class WalkService : IWalkRepository
    {
        protected readonly NZWalksDbContext _context;

        public WalkService(NZWalksDbContext context)
        {
            _context = context;
        }

        public async Task<Walk> CreateAsync(Walk data) 
        { 
            await _context.Walks.AddAsync(data);
            await _context.SaveChangesAsync();

            return data;
        }

        public async Task<Walk?> DeleteAsync(Guid id)
        {
            var walk = await _context.Walks.FirstOrDefaultAsync(x => x.Id == id);

            if (walk == null) return null;

            _context.Walks.Remove(walk);
            await _context.SaveChangesAsync();

            return walk;
        }

        public async Task<List<Walk>> GetAllAsync(string? filterOn = null, string? filterQuery = null, string? sortBy = null, bool isAscending = true, int pageNumber = 1, int pageSize = 1000)
        {
            var walks = _context.Walks
                .Include(x => x.Difficulty)
                .Include("Region").AsQueryable();

            if (!string.IsNullOrWhiteSpace(filterOn) &&
                !string.IsNullOrWhiteSpace(filterQuery)) 
            {
                if (filterOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    walks = walks.Where(x => x.Name.Contains(filterQuery));
                }
            }

            if (!string.IsNullOrWhiteSpace(sortBy))
            {
                if (sortBy.Equals("Name", StringComparison.OrdinalIgnoreCase)) 
                {
                    walks = isAscending ? walks.OrderBy(x => x.Name) : walks.OrderByDescending(x => x.Name);
                }
                else if (sortBy.Equals("Length", StringComparison.OrdinalIgnoreCase))
                {
                    walks = isAscending ? walks.OrderBy(x => x.LenghtInKm) : walks.OrderByDescending(x => x.LenghtInKm);
                }
            }

            var skipResults = (pageNumber - 1) * pageSize;

            return await walks.Skip(skipResults).Take(pageSize).ToListAsync();
        }

        public async Task<Walk?> GetByIdAsync(Guid id)
        {
            var walk = await _context.Walks
                .Include(x => x.Region)
                .Include(x => x.Difficulty)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (walk == null) 
            {
                return null;
            }

            return walk;
        }

        public async Task<Walk?> UpdateAsync(Guid id, Walk data)
        {
            var walk = await _context.Walks.FirstOrDefaultAsync(x => x.Id == id);

            if (walk == null)
            {
                return null;
            }

            walk.Name = data.Name;
            walk.Description = data.Description;
            walk.LenghtInKm = data.LenghtInKm;
            walk.WalkImageUrl = data.WalkImageUrl;
            walk.DifficultyId = data.DifficultyId;
            walk.RegionId = data.RegionId;

            await _context.SaveChangesAsync();

            return walk;
        }
    }
}
