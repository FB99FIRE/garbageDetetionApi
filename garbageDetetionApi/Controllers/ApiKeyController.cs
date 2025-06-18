using garbageDetetionApi.Context;
using garbageDetetionApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace garbageDetetionApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ApiKeyController(GarbageDbContext context, IConfiguration configuration) : ControllerBase
{
    // GET: api/apikey
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ApiKey>>> GetAllApiKeys()
    {
        var apiKeys = await context.ApiKeys.ToListAsync();
        if (!apiKeys.Any())
        {
            return NotFound("No API keys found.");
        }
        return Ok(apiKeys);
    }
    
    // POST: api/apikey/{type}
    [HttpPost("{type}")]
    public async Task<ActionResult<ApiKey>> CreateApiKey(string type)
    {
        if (!type.Equals("read", StringComparison.CurrentCultureIgnoreCase) && !type.Equals("write", StringComparison.CurrentCultureIgnoreCase))
        {
            return BadRequest("API Key type must be 'read' or 'write'.");
        }
        var apiKey = new ApiKey();
        apiKey.Type = type;
        apiKey.Id = Guid.NewGuid();
        context.ApiKeys.Add(apiKey);
        await context.SaveChangesAsync();
        return CreatedAtAction(nameof(CreateApiKey), new { id = apiKey.Id }, apiKey);
    }
}