using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Data
{
  public class NZWalksDbContext : IdentityDbContext<IdentityUser>
  {
    public NZWalksDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
    {
    }
    //Server=localhost;Database=master;Trusted_Connection=True;
    public DbSet<Difficulty> difficulties { get; set; }
    public DbSet<Region> Regions { get; set; }
    public DbSet<Walk> Walks { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed data for difficulties
            // Easy, Medium, Hard

            var difficulties = new List<Difficulty>()
            {
                new Difficulty()
                {
                    Id = Guid.Parse("3897b275-7a3f-4a84-a620-105b9b0eb89a"),
                    name = "Easy",
                },
                new Difficulty()
                {
                    Id = Guid.Parse("e4567686-1b4d-483d-a374-9e99306c8e7b"),
                    name = "Medium",
                },
                new Difficulty()
                {
                    Id = Guid.Parse("de63304d-8500-4570-8333-abb077e5a23f"),
                    name = "Hard",
                },
            };

            // seed difficulties to the database
            modelBuilder.Entity<Difficulty>().HasData(difficulties);


            var regions = new List<Region>()
            {
                new Region()
                {
                    Id = Guid.Parse("8929b4bf-5be3-4002-8ad6-b9f46f782f16"),
                    Name = "Ciudad de México",
                    Code = "CDMX",
                    RegionImageUrl = "https://th.bing.com/th/id/OIP.YlInlV-4E257n6usFJv1GgHaFJ?pid=ImgDet&rs=1"
                },
                new Region()
                {
                    Id = Guid.Parse("67a04df2-e37a-4e80-8f3b-d52ba62f65e8"),
                    Name = "Guadalajara",
                    Code = "GDL",
                    RegionImageUrl = "https://th.bing.com/th/id/R.3ee144293cd7e27e7d24d62d6ac6da95?rik=ojfVhRArgDl8PA&pid=ImgRaw&r=0"
                },

            };

            // seed Regions to the database
            modelBuilder.Entity<Region>().HasData(regions);

            //MAKE MIGRATIONS TO SEE DATA IN DB

            // Add-Migration "comment"
            // Update-Database



            //seed data for roles and users
            var readerRoleId = "f0571c7a-9d14-48c2-ad57-d08430d9e358";
            var writerRoleId = "8e209f7d-2358-44f1-8623-86684bf3081b";

            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Id = readerRoleId,
                    ConcurrencyStamp = readerRoleId,
                    Name = "Reader",
                    NormalizedName = "Reader".ToUpper()
                },
                new IdentityRole
                {
                    Id = writerRoleId,
                    ConcurrencyStamp = writerRoleId,
                    Name = "Writer",
                    NormalizedName = "Writer".ToUpper()
                }
            }; ;

            modelBuilder.Entity<IdentityRole>().HasData(roles);
        }
    }
}
