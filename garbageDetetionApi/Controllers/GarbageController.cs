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
                if (garbages == null || !garbages.Any())
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

        //api.garbage/{timestamp}
        [HttpGet("{timestamp}")]
        public async Task<ActionResult<Garbage>> GetByTimeToNow(DateTime timestamp)
        {
            return NotFound();
        }

        

        // GET: api/garbage/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Garbage>> GetById(string id)
        {
            return NotFound();
        }

        // POST: api/garbage
        [HttpPost]
        public async Task<IActionResult> PostGarbage(Garbage Garbage)
        {
            return NotFound();
        }
    }
}
