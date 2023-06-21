using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly NZWalksDbContext dbContext;
        public RegionsController(NZWalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        //GET ALL REGIONS
        [HttpGet]
        public IActionResult GetAll()
        {
            // Get data from Database - Domain Models
            var regionsDomain = dbContext.Regions.ToList();

            // Map domain models to DTOs (DATA TRANSFORM OBJECT)
            var regionsDto = new List<RegionDto>();
            foreach (var region in regionsDomain)
            {
                regionsDto.Add(new RegionDto()
                {
                    Id = region.Id,
                    Code = region.Code,
                    Name = region.Name,
                });
            }

            return Ok(regionsDto);
        }

        //GET SINGLE REGION BY ID
        [HttpGet]
        [Route("{id:Guid}")]
        public IActionResult GetById([FromRoute] Guid id)
        {
            //var region = dbContext.Regions.Find(id); same as down, the only difference between is that this one only filter by ID
            var regionDomain = dbContext.Regions.FirstOrDefault(item => item.Id == id);

            if (regionDomain == null)
            {
                return NotFound();
            }

            var regionDto = new RegionFullDto()
            {
                Id = regionDomain.Id,
                Code = regionDomain.Code,
                Name = regionDomain.Name,
                RegionImageUrl = regionDomain.RegionImageUrl,   
            };
            
            return Ok(regionDto);
        }

        //Crate a new region
        [HttpPost]
        public IActionResult CreateRegion([FromBody] AddRegionDto addRegionRequestDto)
        {

            //Map or connvert DTO To domain Model
            var regionDomainModel = new Region()
            {
                Name = addRegionRequestDto.Name,
                Code = addRegionRequestDto.Code,
                RegionImageUrl = addRegionRequestDto.RegionImageUrl,
            };
            // Use domain model to create regionn
            dbContext.Regions.Add(regionDomainModel);
            dbContext.SaveChanges();

            var regionDto = new RegionFullDto
            {
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageUrl = regionDomainModel.RegionImageUrl,
            };

            return CreatedAtAction(nameof(GetById), new { id = regionDto.Id }, regionDto);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public IActionResult UpdateRegion([FromBody] AddRegionDto updateRegionDto, [FromRoute] Guid id)
        {
            var regionFromDomain = dbContext.Regions.FirstOrDefault(item => item.Id == id);
            if (regionFromDomain == null)
            {
                return NotFound();
            }

            regionFromDomain.Name = updateRegionDto.Name;
            regionFromDomain.Code = updateRegionDto.Code;
            regionFromDomain.RegionImageUrl = updateRegionDto.RegionImageUrl;

            dbContext.SaveChanges();

            var regionDto = new RegionFullDto
            {
                Id = regionFromDomain.Id,
                Code = regionFromDomain.Code,
                Name = regionFromDomain.Name,
                RegionImageUrl = regionFromDomain.RegionImageUrl,
            };

            return Ok(regionDto);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public IActionResult deleteRegion([FromRoute] Guid id)
        {
            var regionDomain = dbContext.Regions.FirstOrDefault(item => item.Id == id);
            if (regionDomain == null)
            {
                return NotFound();
            }
            dbContext.Regions.Remove(regionDomain);
            dbContext.SaveChanges();

            var regionDto = new RegionFullDto
            {
                Id = regionDomain.Id,
                Code = regionDomain.Code,
                Name = regionDomain.Name,
                RegionImageUrl = regionDomain.RegionImageUrl,
            };

            return Ok(regionDto);
        }
    }
}
