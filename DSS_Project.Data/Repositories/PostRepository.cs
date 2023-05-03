using DSS_Project.Data.Models;
using DSS_Project.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DSS_Project.Data.Repositories;

public class PostRepository : Repository<Post>, IPostRepository
{
    public PostRepository(ApplicationDbContext context) : base(context)
    {
    }

    public new async Task<Post?> GetByIdAsync(Guid id)
        => await GetSet().AsNoTracking().Include(p => p.Comments).ThenInclude(c => c.CommentRatings).FirstOrDefaultAsync(p => p.Id == id);
}
