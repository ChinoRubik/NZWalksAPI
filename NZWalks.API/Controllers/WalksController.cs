using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.DTO;
using NZWalks.API.Models.Domain;
using NZWalks.API.Repositories;

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
        public async Task<IActionResult> CreateWalk([FromBody] AddWalkDto addWalkDto)
        {
            var walkDomainModel = mapper.Map<Walk>(addWalkDto);
            await walkRepository.AddWalkAsync(walkDomainModel);
            var walkDtoMap = mapper.Map<WalkDto>(walkDomainModel);

            return Ok(walkDtoMap);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllWalks()
        {
            var walksDomain = await walkRepository.getAllWalkAsync();
            var walkDtos = mapper.Map<List<WalkDto>>(walksDomain);

            return Ok(walkDtos);
        }
    }
}
