using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.DTO;
using NZWalks.API.Models.Domain;
using NZWalks.API.Repositories;
using NZWalks.API.CustomActionFilters;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IWalkRepository walkRepository;
        public WalksController(IMapper mapper, IWalkRepository walkRepository)
        {
            this.mapper = mapper;
            this.walkRepository = walkRepository;
        }


        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> CreateWalk([FromBody] AddWalkDto addWalkDto)
        {
            var walkDomainModel = mapper.Map<Walk>(addWalkDto);
            await walkRepository.AddWalkAsync(walkDomainModel);
            var walkDtoMap = mapper.Map<WalkDto>(walkDomainModel);

            return Ok(walkDtoMap);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllWalks([FromQuery] string? filterOn, [FromQuery] string? query)
        {
            var walksDomain = await walkRepository.getAllWalkAsync(filterOn, query);
            var walkDtos = mapper.Map<List<WalkDto>>(walksDomain);

            return Ok(walkDtos);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> getOneWalk([FromRoute] Guid id)
        {
            var walkDomain = await walkRepository.getOneWalkAsync(id);
            if (walkDomain == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<WalkDto>(walkDomain));
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> deleteWalk([FromRoute] Guid id)
        {
            var walkDomain = await walkRepository.deleteWalkAsync(id);
            if (walkDomain == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<WalkDto>(walkDomain));
        }

        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
        public async Task<IActionResult> updateTask([FromRoute] Guid id, [FromBody] UpdateWalkDto updateDto)
        {
            var walkDomain = mapper.Map<Walk>(updateDto);

            var walkDomainUpdated = await walkRepository.updateWalkAsync(id, walkDomain);
            if (walkDomainUpdated == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<UpdateWalkDto>(walkDomainUpdated));
        }
    }
}
