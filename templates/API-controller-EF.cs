using Microsoft.AspNetCore.Mvc;

// Standard web-api
namespace MyApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MyEntityController : ControllerBase
    {
        private static readonly List<MyEntity> _entities = new List<MyEntity>();

        // GET: api/myentity
        [HttpGet]
        public ActionResult<IEnumerable<MyEntity>> Get()
        {
            return Ok(_entities);
        }

        // GET: api/myentity/5
        [HttpGet("{id}")]
        public ActionResult<MyEntity> Get(int id)
        {
            var entity = _entities.FirstOrDefault(e => e.Id == id);
            if (entity == null) return NotFound();
            return Ok(entity);
        }

        // POST: api/myentity
        [HttpPost]
        public ActionResult Create([FromBody] MyEntity entity)
        {
            _entities.Add(entity);
            return CreatedAtAction(nameof(Get), new { id = entity.Id }, entity);
        }

        // PUT: api/myentity/5
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] MyEntity entity)
        {
            var existingEntity = _entities.FirstOrDefault(e => e.Id == id);
            if (existingEntity == null) return NotFound();
            existingEntity.Name = entity.Name;
            return NoContent();
        }

        // DELETE: api/myentity/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var entity = _entities.FirstOrDefault(e => e.Id == id);
            if (entity == null) return NotFound();
            _entities.Remove(entity);
            return NoContent();
        }
    }

    public class MyEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
