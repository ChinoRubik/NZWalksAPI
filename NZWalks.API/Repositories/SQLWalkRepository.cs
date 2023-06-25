using Microsoft.EntityFrameworkCore;
using NZWalks.API.Models.Domain;
using NZWalks.API.Data;
using NZWalks.API.Models.DTO;

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

        public async Task<Walk?> deleteWalkAsync(Guid id)
        {
            var walkDomain = await dbContext.Walks.Include("Difficulty").Include("Region").FirstOrDefaultAsync(i => i.Id == id);
            if (walkDomain == null)
            {
                return null;
            }
            dbContext.Walks.Remove(walkDomain);
            await dbContext.SaveChangesAsync();
            return walkDomain;
        }

        public async Task<List<Walk>> getAllWalkAsync()
        {
            return await dbContext.Walks.Include("Difficulty").Include("Region").ToListAsync();
        }

        public async Task<Walk?> getOneWalkAsync(Guid id)
        {
            var walkDomain = await dbContext.Walks.Include("Difficulty").Include("Region").FirstOrDefaultAsync(i => i.Id == id);
            return walkDomain;
        }

        public async Task<Walk?> updateWalkAsync(Guid id, Walk Walk)
        {
            var walkDomain = await dbContext.Walks.Include("Difficulty").Include("Region").FirstOrDefaultAsync(i => i.Id == id);
            if (walkDomain == null)
            {
                return null;
            }
            walkDomain.Name = Walk.Name;
            walkDomain.LengthInKm = Walk.LengthInKm;
            walkDomain.Description = Walk.Description;
            walkDomain.WalkImageUrl = Walk.WalkImageUrl;
            walkDomain.DifficultyId = Walk.DifficultyId;
            walkDomain.RegionId = Walk.RegionId;

            await dbContext.SaveChangesAsync();

            return walkDomain;
        }
    }
}
