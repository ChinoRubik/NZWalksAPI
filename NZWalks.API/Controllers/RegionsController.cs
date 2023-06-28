using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.CustomActionFilters;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;
        public RegionsController(IRegionRepository regionRepository, IMapper mapper)
        {
            this.regionRepository = regionRepository;
            this.mapper = mapper;
        }

        //GET ALL REGIONS
        [HttpGet]
        [Authorize(Roles = "Reader, Writer")]
        public async Task<IActionResult> GetAll()
        {
            // Get data from Database - Domain Models
            var regionsDomain = await regionRepository.GetAllAsync();

            // Map domain models to DTOs (DATA TRANSFORM OBJECT)

            //var regionsDto = new List<RegionDto>();
            //foreach (var region in regionsDomain)
            //{
            //    regionsDto.Add(new RegionDto()
            //    {
            //        Id = region.Id,
            //        Code = region.Code,
            //        Name = region.Name,
            //    });
            //}

            //Instead for all regiosDomain to pass it to regionsDto, we can Map and do it in one line
            var regionsDto = mapper.Map<List<RegionDto>>(regionsDomain);

            return Ok(regionsDto);
        }

        //GET SINGLE REGION BY ID
        [HttpGet]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Reader, Writer")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            //var region = dbContext.Regions.Find(id); same as down, the only difference between is that this one only filter by ID
            var regionDomain = await regionRepository.GetOneRegionAsync(id);

            if (regionDomain == null)
            {
                return NotFound();
            }

            //var regionDto = new RegionFullDto()
            //{
            //    Id = regionDomain.Id,
            //    Code = regionDomain.Code,
            //    Name = regionDomain.Name,
            //    RegionImageUrl = regionDomain.RegionImageUrl,   
            //};

            var regionDto = mapper.Map<RegionFullDto>(regionDomain);
            
            return Ok(regionDto);
        }

        //Crate a new region
        [HttpPost]
        [ValidateModel]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> CreateRegion([FromBody] AddRegionDto addRegionRequestDto)
        {
            //Map or connvert DTO To domain Model
            //var regionDomainModel = new Region()
            //{
            //    Name = addRegionRequestDto.Name,
            //    Code = addRegionRequestDto.Code,
            //    RegionImageUrl = addRegionRequestDto.RegionImageUrl,
            //};
            var regionDomainModel = mapper.Map<Region>(addRegionRequestDto);


            // Use domain model to create regionn
            await regionRepository.AddRegionAsync(regionDomainModel);
            //var regionDto = new RegionFullDto
            //{
            //    Id = regionDomainModel.Id,
            //    Code = regionDomainModel.Code,
            //    Name = regionDomainModel.Name,
            //    RegionImageUrl = regionDomainModel.RegionImageUrl,
            //};

            var regionDto = mapper.Map<RegionFullDto>(regionDomainModel);

            return CreatedAtAction(nameof(GetById), new { id = regionDto.Id }, regionDto);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> UpdateRegion([FromBody] AddRegionDto updateRegionDto, [FromRoute] Guid id)
        {
            //MAP DTO to Domain Model
            //var regionDomainModel = new Region
            //{
            //    Code = updateRegionDto.Code,
            //    Name = updateRegionDto.Name,
            //    RegionImageUrl = updateRegionDto.RegionImageUrl,
            //};
            var regionDomainModel = mapper.Map<Region>(updateRegionDto);


            var regionFromDomain = await regionRepository.UpdateRegionAsync(id, regionDomainModel);
            if (regionFromDomain == null)
            {
                return NotFound();
            }

            //var regionDto = new RegionFullDto
            //{
            //    Id = regionFromDomain.Id,
            //    Code = regionFromDomain.Code,
            //    Name = regionFromDomain.Name,
            //    RegionImageUrl = regionFromDomain.RegionImageUrl,
            //};
            var regionDto = mapper.Map<RegionFullDto>(regionFromDomain);


            return Ok(regionDto);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> deleteRegion([FromRoute] Guid id)
        {
            var regionDomain = await regionRepository.DeleteRegion(id);
            if (regionDomain == null)
            {
                return NotFound();
            }

            //var regionDto = new RegionFullDto
            //{
            //    Id = regionDomain.Id,
            //    Code = regionDomain.Code,
            //    Name = regionDomain.Name,
            //    RegionImageUrl = regionDomain.RegionImageUrl,
            //};
            var regionDto = mapper.Map<RegionFullDto>(regionDomain);

            return Ok(regionDto);
        }

    }
}
