using Microsoft.EntityFrameworkCore;
using NZWalks.API.Models.Domain;
using NZWalks.API.Data;

namespace NZWalks.API.Repositories
{
    public class SQLWalkRepository : IWalkRepository
    {
        private readonly NZWalksDbContext dbContext;
        public SQLWalkRepository(NZWalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<Walk> AddWalkAsync(Walk addWalkDomain)
        {
            await dbContext.Walks.AddAsync(addWalkDomain);
            await dbContext.SaveChangesAsync();

            return addWalkDomain;
        }

        public async Task<List<Walk>> getAllWalkAsync()
        {
            return await dbContext.Walks.ToListAsync();
        }
    }
}
