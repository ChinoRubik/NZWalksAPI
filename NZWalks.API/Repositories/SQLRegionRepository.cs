using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;


namespace NZWalks.API.Repositories
{
    public class SQLRegionRepository : IRegionRepository
    {
        private readonly NZWalksDbContext dbContext;
        public SQLRegionRepository(NZWalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<Region>> GetAllAsync()
        {
            return await dbContext.Regions.ToListAsync();
        }

        public async Task<Region?> GetOneRegionAsync(Guid id)
        {
            return await dbContext.Regions.FirstOrDefaultAsync(item => item.Id == id);
        }

        public async Task<Region?> UpdateRegionAsync(Guid id, Region updateRegion)
        {
            var regionDomain = await GetOneRegionAsync(id);
            if (regionDomain == null)
            {
                return null;
            }

            regionDomain.Name = updateRegion.Name;
            regionDomain.Code = updateRegion.Code;
            regionDomain.RegionImageUrl = updateRegion.RegionImageUrl;

            await dbContext.SaveChangesAsync();
            return regionDomain;
        }
        public async Task<Region> AddRegionAsync(Region addRegionDomain)
        {
            await dbContext.Regions.AddAsync(addRegionDomain);
            await dbContext.SaveChangesAsync();

            return addRegionDomain;
        }
           
        public async Task<Region?> DeleteRegion(Guid id)
        {
            var regionDomain = await GetOneRegionAsync(id);
            if (regionDomain == null)
            {
                return null;
            }

            dbContext.Regions.Remove(regionDomain);
            await dbContext.SaveChangesAsync();

            return regionDomain;
        }
    }
}
