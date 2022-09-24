using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GOAT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GoatsController : ControllerBase
    {
        private readonly DataContext context;

        public GoatsController(DataContext Context)
        {
            context = Context;
        }
        

        [HttpGet]
        public async Task<ActionResult<List<Goats>>> Get()
        {

            return Ok(await context.Goats.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Goats>> Get(int id)
        {
            var goat = await context.Goats.FindAsync(id); // Goats is table name
            if (goat == null)
                return BadRequest("Goat not found");
            return Ok(goat);
        }

        [HttpPost]
        public async Task<ActionResult<List<Goats>>> AddGoats(Goats goat)
        {
            context.Goats.Add(goat);
            await context.SaveChangesAsync();
            return Ok(await context.Goats.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<Goats>>> UpdateGoats(Goats request)
        {
            var dbGoat = await context.Goats.FindAsync(request.Id);
            if (dbGoat == null)
                return BadRequest("Goat not Found.");


            dbGoat.Name = request.Name;
            dbGoat.FirstName = request.FirstName;
            dbGoat.LastName = request.LastName;
            dbGoat.Century = request.Century;

            await context.SaveChangesAsync();

            return Ok(await context.Goats.ToListAsync());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Goats>> Delete(int id)
        {
            var dbGoat = await context.Goats.FindAsync(id);
            if (dbGoat == null)
                return BadRequest("Goat not Found.");

            context.Goats.Remove(dbGoat);
            await context.SaveChangesAsync();
            return Ok(await context.Goats.ToListAsync());
        }
    }
}
