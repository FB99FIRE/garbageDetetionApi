using garbageDetetionApi.Context;
using garbageDetetionApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace garbageDetetionApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GarbageController(GarbageDbContext context) : ControllerBase
    {
        // GET: api/garbage
        [HttpGet]
        public async Task<ActionResult<Garbage>> GetAll()
        {
            try
            {
                var garbages = await context.Garbages.ToListAsync();
                if (!garbages.Any())
                {
                    return NotFound("No garbage records found.");
                }
                return Ok(garbages);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        //  GET: api/garbage/time/{timestamp}
        [HttpGet("time/{timestamp}")]
        public async Task<ActionResult<Garbage>> GetByTimeToNow(DateTime timestamp)
        {
            try
            {
                var garbages = await context.Garbages
                    .Where(g => g.Timestamp >= timestamp)
                    .ToListAsync();

                if (!garbages.Any())
                {
                    return NotFound($"No garbage records found after {timestamp}.");
                }
                return Ok(garbages);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        

        // GET: api/garbage/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Garbage>> GetById(string id)
        {
            try
            {
                if (!Guid.TryParse(id, out var garbageId))
                {
                    return BadRequest("Invalid ID format.");
                }

                var garbage = await context.Garbages.FindAsync(garbageId);
                if (garbage == null)
                {
                    return NotFound($"Garbage record with ID {id} not found.");
                }
                return Ok(garbage);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // POST: api/garbage
        [HttpPost]
        public async Task<IActionResult> PostGarbage(Garbage garbage)
        {
            try
            {
                garbage.Id = Guid.NewGuid(); // Ensure a new ID is generated
                context.Garbages.Add(garbage);
                await context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetById), new { id = garbage.Id }, garbage);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
