using Microsoft.EntityFrameworkCore;
using NZWalksAPI.Data;
using NZWalksAPI.Models.Domain;
using NZWalksAPI.Repositories;

namespace NZWalksAPI.Services
{
    public class RegionService : IRegionRepository
    {
        private readonly NZWalksDbContext _context;

        public RegionService(NZWalksDbContext context)
        {
            _context = context;
        }

        public async Task<Region> CreateAsync(Region region)
        {
            await _context.Regions.AddAsync(region);
            await _context.SaveChangesAsync();

            return region;
        }

        public async Task<Region?> DeleteAsync(Guid id)
        {
            var regionDomain = await _context.Regions.FirstOrDefaultAsync(x => x.Id == id);

            if (regionDomain == null) return null;

            _context.Regions.Remove(regionDomain);
            await _context.SaveChangesAsync();

            return regionDomain;
        }

        public async Task<List<Region>> GetAllAsync()
        {
            return await _context.Regions.ToListAsync();
        }

        public async Task<Region?> GetByIdAsync(Guid id)
        {
            return await _context.Regions.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Region?> UpdateAsync(Guid id, Region region)
        {
            var regionDomainModel = await _context.Regions.FirstOrDefaultAsync(x => x.Id == id);

            if (regionDomainModel == null) return null;

            regionDomainModel.Code = region.Code;
            regionDomainModel.Name = region.Name;
            regionDomainModel.RegionImageUrl = region.RegionImageUrl;

            await _context.SaveChangesAsync();

            return regionDomainModel;
        }
    }
}
