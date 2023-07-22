using API.Data;
using API.Models.Domain;
using API.Models.DTO;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    public class RegionRepository : IRegionRepository
    {
        private readonly ApplicationDbContext _context;

        public RegionRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<Region>> GetAllAsync()
        {
            return await _context.Regions.ToListAsync();
        }

        public async Task<Region?> GetRegionByIdAsync(Guid id)
        {
            var region = await _context.Regions.FirstOrDefaultAsync(r => r.Id == id);
            if (region == null)
            {
                return null;
            }
            
            return region;
        }

        public async Task<Region> CreateRegionAsync(Region region)
        {
            await _context.Regions.AddAsync(region);

            await _context.SaveChangesAsync();

            return region;
        }

        public async Task<Region?> UpdateRegionAsync(Guid id, UpdateRegionDto updateRegionDto)
        {
            var regionFromDb = await _context.Regions.FirstOrDefaultAsync(r => r.Id == id);

            if (regionFromDb != null)
            {
                regionFromDb.Code = updateRegionDto.Code;
                regionFromDb.Name = updateRegionDto.Name;
                regionFromDb.RegionImageUrl = updateRegionDto.RegionImageUrl;

            }
            else
            {
                return null;
            }

            await _context.SaveChangesAsync();

            return new Region()
            {
                Id = id,
                Code = regionFromDb.Code,
                Name = regionFromDb.Name,
                RegionImageUrl = regionFromDb.RegionImageUrl,
            };
        }

        public async Task<bool> DeleteRegionAsync(Guid id)
        {
            var region = await _context.Regions.SingleOrDefaultAsync(r => r.Id == id);

            if (region == null)
            {
                return false;
            }

            _context.Regions.Remove(region);

            await _context.SaveChangesAsync();

            return true;
        }
    }
}
