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
    }
}
