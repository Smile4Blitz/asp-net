public class Example {
    public bool IsDeleted { get; set; }
}
public override void SaveChanges() {
    ChangeTracker.Entries()
        .Where(e => e.Entity is Example && e.State == EntityState.Deleted)
        .ToList()
        .ForEach(e => {
            e.State = EntityState.Modified;
            ((Example)e.Entity).IsDeleted = true;
        });
}
