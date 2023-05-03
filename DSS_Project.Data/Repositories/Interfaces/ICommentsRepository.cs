using DSS_Project.Data.Models;

namespace DSS_Project.Data.Repositories.Interfaces;

public interface ICommentsRepository: IRepository<Comment>
{
    Task<IEnumerable<Comment>> GetCommentsForPost(Guid postId);
}
