using DSS_Project.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace DSS_Project.Data.Repositories.Interfaces;

public interface IRepository<T>
    where T: BaseModel
{
    public Task<T?> GetByIdAsync(Guid id);
    public Task<List<T>> GetListAsync(int page, int pageSize);
    public Task CreateAsync(T entity);
    public Task UpdateAsync(T entity);
    public Task SoftDelete(T entity);
    public Task HardDeleteAsync(T entity);
}
