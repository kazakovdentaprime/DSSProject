using DSS_Project.Data.Models;
using DSS_Project.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DSS_Project.Data.Repositories;

public class CommentsRepository : Repository<Comment>, ICommentsRepository
{
    public CommentsRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Comment>> GetCommentsForPost(Guid postId)
        => await GetSet().Where(c => c.PostId == postId).ToListAsync();
}
