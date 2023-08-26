using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalksAPI.CostumeActionFilters;
using NZWalksAPI.Data;
using NZWalksAPI.Models.Data.Regiones;
using NZWalksAPI.Models.Domain;
using NZWalksAPI.Repositories;
using System.Text.Json;

namespace NZWalksAPI.Controllers
{
    // https://localhost:123/api/Regions
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        protected readonly IRegionRepository regionRepository;
        protected readonly IMapper mapper;
        private readonly ILogger<RegionsController> logger;

        public RegionsController(IRegionRepository regionRepository, IMapper mapper, ILogger<RegionsController> logger) 
        {
            this.regionRepository = regionRepository;
            this.mapper = mapper;
            this.logger = logger;
        }

        // GET: https://localhost:[port]/api/regions
        [HttpGet]
        //[Authorize(Roles = "Reader, Writer")]
        public async Task<IActionResult> GetAll()
        {
            logger.LogInformation("GetAll Regions Action Method was invoked");

            var regions = await regionRepository.GetAllAsync();

            if (regions.Count == 0) return NotFound();

            var regionsData = mapper.Map<List<RegionData>>(regions);

            logger.LogInformation($"Finnished GetAllRegions with data: {JsonSerializer.Serialize(regionsData)}");

            return Ok(regionsData);
        }

        // https://localhost:[port]/api/regions/:id
        [HttpGet]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Reader")]
        public async Task<IActionResult> GetById([FromRoute]Guid id)
        {
            var region = await regionRepository.GetByIdAsync(id);

            if (region == null) return NotFound();

            var regionsData = mapper.Map<RegionData>(region);

            return Ok(regionsData);
        }

        // https://localhost:[port]/api/regions
        [HttpPost]
        [ValidateModel]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Create([FromBody] AddRegionData region)
        {
            var regiomDomainModel = mapper.Map<Region>(region);

            regiomDomainModel = await regionRepository.CreateAsync(regiomDomainModel);

            var regionDto = mapper.Map<AddRegionData>(regiomDomainModel);

            return CreatedAtAction(nameof(GetById), new { id = regiomDomainModel.Id }, regionDto);
        }

        // https://localhost:[port]/api/regions/:id
        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionData regionUpdate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var regionDomainModel = mapper.Map<Region>(regionUpdate);

            regionDomainModel = await regionRepository.UpdateAsync(id, regionDomainModel);

            if(regionDomainModel == null) return NotFound();

            var regionDto = mapper.Map<UpdateRegionData>(regionUpdate);

            return Ok(regionDto);
        }

        // https://localhost:[port]/api/regions/:id
        [HttpDelete]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Delete([FromRoute]Guid id) 
        {
            var regionDomain = await regionRepository.DeleteAsync(id);

            if (regionDomain == null) return NotFound();

            return Ok();
        }
    }
}
