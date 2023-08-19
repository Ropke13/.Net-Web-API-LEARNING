﻿using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalksAPI.Models.Domain;
using NZWalksAPI.Models.DTO;
using NZWalksAPI.Repositories;

namespace NZWalksAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        protected readonly IWalkRepository walkRepository;
        protected readonly IMapper mapper;

        public WalksController(IWalkRepository walkRepository, IMapper mapper)
        {
            this.walkRepository = walkRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var walks = await walkRepository.GetAllAsync();

            if (walks.Count == 0) return NotFound();

            var walksData = mapper.Map<List<WalkData>>(walks);

            return Ok(walksData);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var walk = await walkRepository.GetByIdAsync(id);

            if (walk == null) return NotFound();

            var walkData = mapper.Map<WalkData>(walk);

            return Ok(walkData);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddWalkData data)
        {
            var walkDomainModel = mapper.Map<Walk>(data);
            walkDomainModel = await walkRepository.CreateAsync(walkDomainModel);

            var walkData = mapper.Map<WalkData>(walkDomainModel);

            //return Ok(walkData);
            return CreatedAtAction(nameof(GetById), new { id = walkDomainModel.Id }, walkData);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateWalkData data)
        {
            var walk = mapper.Map<Walk>(data);

            walk = await walkRepository.UpdateAsync(id, walk);

            if (walk == null) return NotFound();

            var walkData = mapper.Map<WalkData>(walk);

            return Ok(walkData);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var walk = await walkRepository.DeleteAsync(id);

            if (walk == null) return NotFound();

            return Ok(walk);
        }

    }
}