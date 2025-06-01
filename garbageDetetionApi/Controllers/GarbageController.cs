using System.Text.Json;
using garbageDetetionApi.Context;
using garbageDetetionApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace garbageDetetionApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GarbageController(GarbageDbContext context, IConfiguration configuration) : ControllerBase
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
        
        //  GET: api/garbage/count/{amount}
        [HttpGet("count/{amount}")]
        public async Task<ActionResult<Garbage>> GetByAmount(int amount)
        {
            try
            {
                if (amount <= 0)
                {
                    return BadRequest("Amount must be greater than zero.");
                }

                var garbages = await context.Garbages
                    .OrderByDescending(g => g.Timestamp)
                    .Take(amount)
                    .ToListAsync();

                if (!garbages.Any())
                {
                    return NotFound($"No garbage records found for the last {amount} entries.");
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
            string apiUrl = configuration["apiUrl"];
            
            try
            {
                using var httpClient = new HttpClient();
                var response = await httpClient.GetAsync(apiUrl);

                if (!response.IsSuccessStatusCode)
                {
                    return StatusCode((int)response.StatusCode, "Failed to retrieve weather data.");
                }

                var json = await response.Content.ReadAsStringAsync();
                var weatherResponse = JsonDocument.Parse(json).RootElement;

                garbage.Id = Guid.NewGuid();
                garbage.Detected = garbage.Detected;
                garbage.Confidence_score = garbage.Confidence_score;
                garbage.Weather = weatherResponse.GetProperty("weather")[0].GetProperty("main").GetString();
                garbage.Temp = Convert.ToDecimal(weatherResponse.GetProperty("main").GetProperty("temp").GetDouble());
                garbage.Humidity = Convert.ToDecimal(weatherResponse.GetProperty("main").GetProperty("humidity").GetInt32());
                garbage.Windspeed = Convert.ToDecimal(weatherResponse.GetProperty("wind").GetProperty("speed").GetDouble());
                garbage.Timestamp = DateTime.UtcNow;

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
