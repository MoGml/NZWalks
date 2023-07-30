using API.Models.Domain;

namespace API.Repositories
{
    public interface IWalkRepository
    {
        Task<List<Walk>> GetWalkListAsync(string? FilterOn, string? QueryTerm, string? SortBy, bool IsAsscending, int PageIndex, int PageSize );

        Task<Walk?> GetWalkByIdAsync(Guid id);

        Task<Walk> CreateWalkAsync(Walk walk);

        Task<Walk?> UpdateWalkAsync(Guid walkId , Walk walk);

        Task<bool> DeleteWalkAsync(Guid id);
    }
}
