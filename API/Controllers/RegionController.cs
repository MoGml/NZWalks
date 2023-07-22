using API.Data;
using API.Models.Domain;
using API.Models.DTO;
using API.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionController : ControllerBase
    {
        private readonly IRegionRepository _regionRepository;
        private readonly IMapper _mapper;

        public RegionController( IRegionRepository regionRepository, IMapper mapper)
        {
            _regionRepository = regionRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Get All Regions
        /// </summary>
        /// <returns>List of Regions</returns>
        [HttpGet]
        public async Task<ActionResult<List<RegionDto>>> GetAllRegions()
        {

            var regions = await _regionRepository.GetAllAsync();

            var regionsDto = new List<RegionDto>();

            regionsDto = _mapper.Map<List<RegionDto>>(regions);

            return Ok(regionsDto);
        }


        /// <summary>
        /// Get a Region from db by id of this region
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Region Object</returns>
        [HttpGet("{id:Guid}")]
        public async Task<ActionResult<RegionDto>> GetRegion(Guid id)
        {
            var region = await _regionRepository.GetRegionByIdAsync(id);

            if (region == null)
            {
                return NotFound();
            }

            var regionDto = _mapper.Map<RegionDto>(region);

            return Ok(regionDto);
        }


        /// <summary>
        /// Create a Region
        /// </summary>
        /// <param name="region"></param>
        /// <returns>The Created Region if created</returns>
        [HttpPost]
        public async Task<ActionResult<RegionDto>> AddRegion(NewRegionDto newRegionDto)
        {
            var regionDomainModel = _mapper.Map<Region>(newRegionDto);

            regionDomainModel = await _regionRepository.CreateRegionAsync(regionDomainModel);

            var regionDto = _mapper.Map<RegionDto>(regionDomainModel);

            regionDto.Id = regionDomainModel.Id;

            return CreatedAtAction(nameof(GetRegion), new { id = regionDomainModel.Id }, regionDto);
        }


        [HttpPut("{id}")]
        public async Task<ActionResult<Region>> UpdateRegion(Guid id, UpdateRegionDto updateRegionDto)
        {
            var regionAfterUpdate = await _regionRepository.UpdateRegionAsync(id, updateRegionDto);

            if (regionAfterUpdate == null)
            {
                return NotFound();
            }

            var regionDto = _mapper.Map<RegionDto>(regionAfterUpdate);

            return Ok(regionDto);
        }


        /// <summary>
        /// Delete Region By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>200 on Success</returns>
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<ActionResult> DeleteRegion(Guid id)
        {
            if (await _regionRepository.DeleteRegionAsync(id) == true)
            {
                return Ok("Deleted");
            }
            else
                return NotFound();
        }
    }
}
