using Microsoft.EntityFrameworkCore;
using NZWalksAPI.Data;
using NZWalksAPI.Models.Domain;
using NZWalksAPI.Models.DTO;
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

        public async Task<List<Walk>> GetAllAsync()
        {
            var items = await _context.Walks
                .Include(x => x.Difficulty)
                .Include("Region")
                .ToListAsync();

            return items;
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
