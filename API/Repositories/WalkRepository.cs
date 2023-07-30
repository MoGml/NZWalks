using API.Data;
using API.Models.Domain;
using API.Models.DTO;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
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
        public async Task<List<Walk>> GetWalkListAsync(
            string? FilterOn, string? QueryTerm, 
            string? SortBy, bool IsAsscending, 
            int PageIndex, int PageSize)
        {
            var walks = _context.Walks
                .Include(w => w.Difficulty)
                .Include(w => w.Region)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(FilterOn) && !string.IsNullOrEmpty(QueryTerm))
            {
                if (FilterOn.ToLower() == "name")
                {
                    walks = walks.Where(w => w.Name.Contains(QueryTerm));
                }
            }

            if (!string.IsNullOrWhiteSpace(SortBy))
            {
                if(SortBy.ToLower() == "name")
                {
                    walks = IsAsscending ? walks.OrderBy(w => w.Name) : walks.OrderByDescending(w => w.Name);
                }
                
                if(SortBy.ToLower() == "length")
                {
                    walks = IsAsscending ? walks.OrderBy(w => w.LengthInKm) : walks.OrderByDescending(w => w.LengthInKm);
                }
            }

            var count = walks.Count();

            var skip = (PageIndex - 1) * PageSize;

            var remaining = count - skip;

            return await walks
                .Skip(skip)
                .Take(PageSize > remaining ? remaining : PageSize)
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
