using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// Controller using Entity Framework
namespace MyApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MyEntityController : ControllerBase
    {
        private readonly MyDbContext _context;

        public MyEntityController(MyDbContext context)
        {
            _context = context;
        }

        // GET: api/myentity
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MyEntity>>> Get()
        {
            return await _context.MyEntities.ToListAsync();
        }

        // GET: api/myentity/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MyEntity>> Get(int id)
        {
            var entity = await _context.MyEntities.FindAsync(id);
            if (entity == null) return NotFound();
            return Ok(entity);
        }

        // POST: api/myentity
        [HttpPost]
        public async Task<ActionResult<MyEntity>> Post([FromBody] MyEntity entity)
        {
            _context.MyEntities.Add(entity);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = entity.Id }, entity);
        }

        // PUT: api/myentity/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] MyEntity entity)
        {
            if (id != entity.Id) return BadRequest();
            _context.Entry(entity).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.MyEntities.Any(e => e.Id == id)) return NotFound();
                throw;
            }
            return NoContent();
        }

        // DELETE: api/myentity/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var entity = await _context.MyEntities.FindAsync(id);
            if (entity == null) return NotFound();
            _context.MyEntities.Remove(entity);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }

    public class MyEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options) { }
        public DbSet<MyEntity> MyEntities { get; set; }
    }
}
