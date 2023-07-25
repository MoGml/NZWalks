using API.Models.Domain;

namespace API.Repositories
{
    public interface IWalkRepository
    {
        Task<List<Walk>> GetWalkListAsync();

        Task<Walk?> GetWalkByIdAsync(Guid id);

        Task<Walk> CreateWalkAsync(Walk walk);

        Task<Walk?> UpdateWalkAsync(Guid walkId , Walk walk);

        Task<bool> DeleteWalkAsync(Guid id);
    }
}
