using API.Data;
using API.Models.Domain;
using API.Models.DTO;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Walk = API.Models.Domain.Walk;

namespace API.Repositories
{
    public class WalkRepository : IWalkRepository
    {
        private readonly ApplicationDbContext _context;

        public WalkRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<Walk>> GetWalkListAsync()
        {
            return await _context.Walks
                .Include(w => w.Difficulty)
                .Include(w => w.Region)
                .ToListAsync();
        }

        public async Task<Walk?> GetWalkByIdAsync(Guid id)
        {
            return await _context.Walks
                .Include(w => w.Difficulty)
                .Include(w => w.Region)
                .FirstOrDefaultAsync(w => w.Id == id) ?? null;
        }

        public async Task<Walk> CreateWalkAsync(Walk walk)
        {
            await _context.Walks.AddAsync(walk);
            await _context.SaveChangesAsync();

            return walk;
        }

        public async Task<Walk?> UpdateWalkAsync(Guid walkId, Walk walk)
        {
            var walkInDb = await _context.Walks.SingleOrDefaultAsync(w => w.Id == walkId);

            if (walkInDb == null)
            {
                return walk;
            }

            walkInDb.Name = walk.Name;
            walkInDb.Description = walk.Description;
            walkInDb.LengthInKm = walk.LengthInKm;
            walkInDb.WalkImageUrl = walk.WalkImageUrl;
            walkInDb.RegionId = walk.RegionId;
            walkInDb.DifficultyId = walk.DifficultyId;

            //_context.Walks.Update(walk);

            await _context.SaveChangesAsync();

            return walk;
        }

        public async Task<bool> DeleteWalkAsync(Guid id)
        {
            var walk = await _context.Walks.SingleOrDefaultAsync(w => w.Id == id);

            if (walk == null)
            {
                return false;
            }

            _context.Walks.Remove(walk);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
