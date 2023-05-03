using DSS_Project.Data.Models;
using DSS_Project.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DSS_Project.Data.Repositories;

public class UserRepository : Repository<User>, IUserRepository
{
    public UserRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<User?> GetUserByEmailAsync(string email)
        => await GetSet().FirstOrDefaultAsync(u => u.Email == email);
}
