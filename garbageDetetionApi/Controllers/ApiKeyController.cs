using garbageDetetionApi.Context;
using Microsoft.AspNetCore.Mvc;

namespace garbageDetetionApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ApiKeyController(GarbageDbContext context, IConfiguration configuration) : ControllerBase
{
    
}