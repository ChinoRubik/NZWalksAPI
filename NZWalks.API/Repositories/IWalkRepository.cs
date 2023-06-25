using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public interface IWalkRepository
    {
        Task<Walk> AddWalkAsync(Walk addWalkDomain);
        Task<List<Walk>> getAllWalkAsync();
        Task<Walk?> getOneWalkAsync(Guid id);
        Task<Walk?> deleteWalkAsync(Guid id);
    }
}
