using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;

namespace NZWalks.API.Repositories
{
    public interface IWalkRepository
    {
        Task<Walk> AddWalkAsync(Walk addWalkDomain);
        Task<List<Walk>> getAllWalkAsync(string? filterOn = null, string? query = null, 
            string? orderBy = null, bool isAscending = true, int pageNumber = 1, int pageSize = 1000);
        Task<Walk?> getOneWalkAsync(Guid id);
        Task<Walk?> deleteWalkAsync(Guid id);
        Task<Walk?> updateWalkAsync(Guid id, Walk Walk);
    }
}
