using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalksAPI.CostumeActionFilters;
using NZWalksAPI.Data;
using NZWalksAPI.Models.Domain;
using NZWalksAPI.Models.DTO;
using NZWalksAPI.Repositories;

namespace NZWalksAPI.Controllers
{
    // https://localhost:123/api/Regions
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RegionsController : ControllerBase
    {
        protected readonly IRegionRepository regionRepository;
        protected readonly IMapper mapper;

        public RegionsController(IRegionRepository regionRepository, IMapper mapper) 
        {
            this.regionRepository = regionRepository;
            this.mapper = mapper;
        }

        // GET: https://localhost:[port]/api/regions
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var regions = await regionRepository.GetAllAsync();

            if (regions.Count == 0) return NotFound();

            var regionsData = mapper.Map<List<RegionDto>>(regions);

            return Ok(regionsData);
        }

        // https://localhost:[port]/api/regions/:id
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute]Guid id)
        {
            var region = await regionRepository.GetByIdAsync(id);

            if (region == null) return NotFound();

            var regionsData = mapper.Map<RegionDto>(region);

            return Ok(regionsData);
        }

        // https://localhost:[port]/api/regions
        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> Create([FromBody] AddRegionRequestDto region)
        {
            var regiomDomainModel = mapper.Map<Region>(region);

            regiomDomainModel = await regionRepository.CreateAsync(regiomDomainModel);

            var regionDto = mapper.Map<AddRegionRequestDto>(regiomDomainModel);

            return CreatedAtAction(nameof(GetById), new { id = regiomDomainModel.Id }, regionDto);
        }

        // https://localhost:[port]/api/regions/:id
        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto regionUpdate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var regionDomainModel = mapper.Map<Region>(regionUpdate);

            regionDomainModel = await regionRepository.UpdateAsync(id, regionDomainModel);

            if(regionDomainModel == null) return NotFound();

            var regionDto = mapper.Map<UpdateRegionRequestDto>(regionUpdate);

            return Ok(regionDto);
        }

        // https://localhost:[port]/api/regions/:id
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute]Guid id) 
        {
            var regionDomain = await regionRepository.DeleteAsync(id);

            if (regionDomain == null) return NotFound();

            return Ok();
        }
    }
}
