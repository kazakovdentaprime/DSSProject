using DSS_Project.Data.Models;
using DSS_Project.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DSS_Project.Data.Repositories;

public class Repository<T> : IRepository<T>
    where T: BaseModel
{
    public Repository(ApplicationDbContext context)
    {
        _context = context;
    }

    protected ApplicationDbContext _context;

    protected DbSet<T> GetSet()
        => _context.Set<T>();

    protected void AttachEntity(T entity, EntityState entityState = EntityState.Modified)
    {
        GetSet().Attach(entity);
        var entry = _context.Entry(entity);
        entry.State = entityState;
    }

    public async Task<T?> GetByIdAsync(Guid id)
        => await GetSet().AsNoTracking().FirstOrDefaultAsync(e => e.Id == id);

    public async Task<List<T>> GetListAsync(int page, int pageSize) 
        => await GetSet()
            .AsNoTracking()
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

    public async Task CreateAsync(T entity)
    {
        if (entity.Id != default)
        {
            throw new ArgumentException("Entity already exists");
        }

        entity.Id = Guid.NewGuid();
        entity.CreatedAt = DateTime.UtcNow;
        await _context.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(T entity)
    {
        AttachEntity(entity);
        entity.UpdatedAt = DateTime.UtcNow;
        await _context.SaveChangesAsync();
    }

    public async Task SoftDelete(T entity)
    {
        AttachEntity(entity);
        entity.DeletedAt = DateTime.UtcNow;
        await _context.SaveChangesAsync();
    }

    public async Task HardDeleteAsync(T entity)
    {
        AttachEntity(entity, EntityState.Deleted);
        entity.DeletedAt = DateTime.UtcNow;
        await _context.SaveChangesAsync();
    }
}
