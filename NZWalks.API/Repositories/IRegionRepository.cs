using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public interface IRegionRepository
    {
        Task<List<Region>> GetAllAsync();
        Task<Region?> GetOneRegionAsync(Guid id);
        Task<Region> AddRegionAsync(Region addRegionDomain);
        Task<Region?> UpdateRegionAsync(Guid id, Region updateRegion);
        Task<Region?> DeleteRegion(Guid id);
    }
}
