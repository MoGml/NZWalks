using API.Data;
using API.Models.Domain;
using API.Models.DTO;
using API.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalkController : ControllerBase
    {
        private readonly IWalkRepository _walkRepository;
        private readonly IMapper _mapper;

        public WalkController(IWalkRepository walkRepository, IMapper mapper)
        {
            _walkRepository = walkRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Get List of Walks
        /// </summary>
        /// <returns>List of WalksDto</returns>
        [HttpGet]
        public async Task<ActionResult<List<WalkDTO>>> GetAllWalks()
        {
            return _mapper.Map<List<WalkDTO>>( await _walkRepository.GetWalkListAsync());
        }



        [HttpGet("{id}")]
        public async Task<ActionResult<WalkDTO>> GetWalkById(Guid id)
        {
            var walk = _mapper.Map<WalkDTO>(await _walkRepository.GetWalkByIdAsync(id));

            if (walk == null) return NotFound();

            return walk;
        }

        // Need to handle in correct add difficulty id and region id by user
        [HttpPost]
        public async Task<ActionResult<WalkDTO>> CreateWalk(NewWalkDto newWalkDto)
        {
            var walk = _mapper.Map<Walk>(newWalkDto);

            walk = await _walkRepository.CreateWalkAsync(walk);

            if (walk == null)
            {
                return NotFound();
            }

            var walkDto = _mapper.Map<WalkDTO>(walk);

            //return Ok(_mapper.Map<WalkDTO>(walk));

            return CreatedAtAction(nameof(GetWalkById), new { id = walk.Id }, walkDto);

        }

        // Need to handle in correct add difficulty id and region id by user
        [HttpPut]
        public async Task<ActionResult<WalkDTO>> UpdateWalk(Guid id, UpdateWalkDto updateWalkDto)
        {

            var walk = _mapper.Map<Walk>(updateWalkDto);

            walk = await _walkRepository.UpdateWalkAsync(id, walk);

            if (walk == null)
            {
                return NotFound();
            }

            walk = await _walkRepository.GetWalkByIdAsync(id);

            return Ok(_mapper.Map<WalkDTO>(walk));

        }



        [HttpDelete]
        public async Task<ActionResult<bool>> DeleteWalk(Guid id)
        {
            var deleted = await _walkRepository.DeleteWalkAsync(id);

            if (!deleted)
            {
                return NotFound();
            }

            return Ok("deleted");
        }


    }
}
