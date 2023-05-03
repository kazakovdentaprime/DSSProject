using DSS_Project.Data.Models;

namespace DSS_Project.Data.Repositories.Interfaces;

public interface IUserRepository : IRepository<User>
{
    Task<User?> GetUserByEmailAsync(string email);
}
