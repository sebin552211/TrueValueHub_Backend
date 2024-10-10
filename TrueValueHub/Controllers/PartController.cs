using Microsoft.AspNetCore.Mvc;
using TrueValueHub.Interfaces;
using TrueValueHub.Models;

namespace TrueValueHub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PartController : ControllerBase
    {
        private readonly IPartService _partService;

        public PartController(IPartService partService)
        {
            _partService = partService;
        }

        // GET: api/parts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Part>>> GetParts()
        {
            var parts = await _partService.GetAllParts();
            return Ok(parts);
        }

        // GET: api/parts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Part>> GetPart(string id)
        {
            var part = await _partService.GetPartByInternalPartNo(id);

            if (part == null)
            {
                return NotFound();
            }

            return Ok(part);
        }

        // POST: api/parts
        [HttpPost]
        public async Task<ActionResult<Part>> PostPart(Part part)
        {
            await _partService.AddPart(part);
            return CreatedAtAction(nameof(GetPart), new { id = part.PartId }, part);
        }

        // PUT: api/parts/5
        [HttpPut("{internalPartNumber}")]
        public async Task<IActionResult> PutPart(string internalPartNumber, Part part)
        {
            var isUpdated = await _partService.UpdatePart(part, internalPartNumber);
            if (isUpdated)
            {
                return Ok("Parts Updated Successfully");
            }
            return BadRequest();
        }

        [HttpPost("{partId}/manufacturings")]
        public async Task<IActionResult> AddManufacturing(int partId, [FromBody] Manufacturing newManufacturing)
        {
            if (newManufacturing == null)
            {
                return BadRequest("Manufacturing cannot be null.");
            }

            var manufacturingId = await _partService.AddManufacturingToPart(partId, newManufacturing);

            if (manufacturingId > 0)
            {
                return Ok(manufacturingId);
            }

            return NotFound("Part not found or manufacturing could not be added.");
        }

        [HttpGet("{partId}/manufacturings")]
        public async Task<ActionResult<IEnumerable<Manufacturing>>> GetManufacturingsByPartId(int partId)
        {
            var manufacturings = await _partService.GetManufacturingsByPartId(partId);
            if (manufacturings == null || !manufacturings.Any())
            {
                return NotFound("No manufacturing records found for this part.");
            }

            return Ok(manufacturings);
        }

        [HttpPut("{partId}/manufacturings/{manufacturingId}")]
        public async Task<IActionResult> UpdateManufacturing(int partId, int manufacturingId, [FromBody] Manufacturing manufacturing)
        {
            if (manufacturing == null)
            {
                return BadRequest("Manufacturing cannot be null.");
            }

            var isUpdated = await _partService.UpdateManufacturing(partId, manufacturingId, manufacturing);
            if (isUpdated)
            {
                return Ok("Manufacturing updated successfully.");
            }

            return NotFound("Manufacturing not found.");
        }

        [HttpDelete("{partId}/manufacturings/{manufacturingId}")]
        public async Task<IActionResult> DeleteManufacturing(int partId, int manufacturingId)
        {
            var isDeleted = await _partService.DeleteManufacturing(partId, manufacturingId);
            if (isDeleted)
            {
                return Ok("Manufacturing deleted successfully.");
            }

            return NotFound("Manufacturing not found.");
        }
    }
}
