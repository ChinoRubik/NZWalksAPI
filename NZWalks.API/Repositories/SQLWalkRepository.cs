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

        public async Task<List<Walk>> getAllWalkAsync(string? filterOn = null, string? query = null,
            string? orderBy = null, bool isAscending = true)
        {
            var walks = dbContext.Walks.Include("Difficulty").Include("Region").AsQueryable();

            //Filtering
            if (string.IsNullOrWhiteSpace(filterOn) == false && string.IsNullOrWhiteSpace(query) == false)
            {
                if (filterOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    walks = walks.Where(i => i.Name.Contains(query));
                } 
                else if (filterOn.Equals("Description", StringComparison.OrdinalIgnoreCase))
                {
                    walks = walks.Where(i => i.Description.Contains(query));
                }
            }

            //Sorting
            if (string.IsNullOrWhiteSpace(orderBy) == false)
            {
                if (orderBy.Equals("name", StringComparison.OrdinalIgnoreCase))
                {
                    walks = isAscending ? walks.OrderBy(i => i.Name) : walks.OrderByDescending(i => i.Name);
                } 
                else if (orderBy.Equals("length", StringComparison.OrdinalIgnoreCase))
                {
                    walks = isAscending ? walks.OrderBy(i => i.LengthInKm) : walks.OrderByDescending(i => i.LengthInKm);
                }
            }
            return await walks.ToListAsync();
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
