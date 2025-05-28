using garbageDetetionApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace garbageDetetionApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GarbageController : ControllerBase
    {
        // GET: api/garbage
        [HttpGet]
        public async Task<ActionResult<Garbage>> GetAll()
        {
        }

        //api.garbage/{timestamp}
        [HttpGet("{timestamp}")]
        public async Task<ActionResult<Garbage>> GetByTimeToNow(DateTime timestamp)
        {

        }

        

        // GET: api/garbage/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Garbage>> GetById(string id)
        {

        }

        // POST: api/garbage
        [HttpPost]
        public async Task<IActionResult> PostGarbage(Garbage Garbage)
        {

        }
    }
}
