public interface IMyEntityService
{
    Task<IEnumerable<MyEntity>> GetAllEntities();
    Task<MyEntity> GetEntityById(int id);
    Task CreateEntity(MyEntity entity);
    Task UpdateEntity(int id, MyEntity entity);
    Task DeleteEntity(int id);
}

public class MyEntityService : IMyEntityService
{
    private readonly MyDbContext _context;

    public MyEntityService(MyDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<MyEntity>> GetAllEntities() =>
        await _context.MyEntities.ToListAsync();

    public async Task<MyEntity> GetEntityById(int id) =>
        await _context.MyEntities.FindAsync(id);

    public async Task CreateEntity(MyEntity entity)
    {
        _context.MyEntities.Add(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateEntity(int id, MyEntity entity)
    {
        if (id != entity.Id) throw new ArgumentException("ID mismatch");
        _context.Entry(entity).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteEntity(int id)
    {
        var entity = await _context.MyEntities.FindAsync(id);
        if (entity == null) throw new KeyNotFoundException("Entity not found");
        _context.MyEntities.Remove(entity);
        await _context.SaveChangesAsync();
    }
}

public class MyEntityController : ControllerBase
{
    private readonly IMyEntityService _service;

    public MyEntityController(IMyEntityService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<MyEntity>>> Get() =>
        Ok(await _service.GetAllEntities());

    [HttpGet("{id}")]
    public async Task<ActionResult<MyEntity>> Get(int id)
    {
        var entity = await _service.GetEntityById(id);
        if (entity == null) return NotFound();
        return Ok(entity);
    }

    [HttpPost]
    public async Task<ActionResult> Post([FromBody] MyEntity entity)
    {
        await _service.CreateEntity(entity);
        return CreatedAtAction(nameof(Get), new { id = entity.Id }, entity);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] MyEntity entity)
    {
        await _service.UpdateEntity(id, entity);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.DeleteEntity(id);
        return NoContent();
    }
}
