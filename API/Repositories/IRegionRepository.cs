using API.Models.Domain;
using API.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace API.Repositories
{
    public interface IRegionRepository
    {
        Task<List<Region>> GetAllAsync();

        Task<Region?> GetRegionByIdAsync(Guid id);

        Task<Region> CreateRegionAsync(Region region);

        Task<Region> UpdateRegionAsync(Guid id, UpdateRegionDto updateRegionDto);

        Task<bool> DeleteRegionAsync(Guid id);
    }
}
