using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalksAPI.Data;
using NZWalksAPI.Models.Domain;
using NZWalksAPI.Models.DTO;

namespace NZWalksAPI.Controllers
{
    // https://localhost:123/api/Regions
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly NZWalksDbContext dbContext;
        public RegionsController(NZWalksDbContext dcContext) 
        {
            this.dbContext = dcContext;
        }

        // GET: https://localhost:[port]/api/regions
        [HttpGet]
        public IActionResult GetAll()
        {
            var regions = dbContext.Regions.ToList();
            
            //Map Entity to data class
            var regionsDto = new List<RegionDto>();
            foreach (var region in regions)
            {
                regionsDto.Add(new RegionDto()
                {
                    Id = region.Id,
                    Code = region.Code,
                    Name = region.Name,
                });
            }
            
            if (regions.Count == 0) return NotFound();

            return Ok(regions);
        }

        // https://localhost:[port]/api/regions/:id
        [HttpGet]
        [Route("{id:Guid}")]
        public IActionResult GetById([FromRoute]Guid id)
        {
            //var region = dbContext.Regions.Find(id);
            var region = dbContext.Regions.FirstOrDefault(x => x.Id == id);

            if (region == null) return NotFound();

            var regionsDto = new RegionDto
            {
                Id = region.Id,
                Code = region.Code,
                Name = region.Name,
            };

            return Ok(region);
        }

        // https://localhost:[port]/api/regions
        [HttpPost]
        public IActionResult Create([FromBody] AddRegionRequestDto region)
        {
            var regiomDomainModel = new Region
            {
                Code = region.Code,
                Name = region.Name,
                RegionImageUrl = region.RegionImageUrl,
            };

            dbContext.Regions.Add(regiomDomainModel);
            dbContext.SaveChanges();

            var regionDto = new RegionDto
            {
                Id = regiomDomainModel.Id,
                Code = regiomDomainModel.Code,
                Name = regiomDomainModel.Name,
                RegionImageUrl = regiomDomainModel.RegionImageUrl,
            };

            return CreatedAtAction(nameof(GetById), new { id = regiomDomainModel.Id }, regionDto);
        }

        // https://localhost:[port]/api/regions/:id
        [HttpPut]
        [Route("{id:Guid}")]
        public IActionResult Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto regionUpdate)
        {
            var regionDomainModel = dbContext.Regions.FirstOrDefault(x => x.Id == id);

            if (regionDomainModel == null) return NotFound();

            regionDomainModel.Code = regionUpdate.Code;
            regionDomainModel.Name = regionUpdate.Name;
            regionDomainModel.RegionImageUrl = regionUpdate.RegionImageUrl;

            dbContext.SaveChanges();

            var regionDto = new RegionDto
            {
                Id = regionDomainModel.Id,
                Code = regionUpdate.Code,
                Name = regionUpdate.Name,
                RegionImageUrl = regionUpdate.RegionImageUrl,
            };

            return Ok(regionDto);
        }

        // https://localhost:[port]/api/regions/:id
        [HttpDelete]
        [Route("{id:Guid}")]
        public IActionResult Delete([FromRoute]Guid id) 
        {
            var regionDomain = dbContext.Regions.FirstOrDefault(x => x.Id == id);

            if (regionDomain == null) return NotFound();

            dbContext.Regions.Remove(regionDomain);
            dbContext.SaveChanges();

            return Ok();
        }
    }
}
