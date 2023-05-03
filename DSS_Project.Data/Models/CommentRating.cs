namespace DSS_Project.Data.Models;

public class CommentRating: BaseModel
{
    public Guid UserId { get; set; }
    public Guid CommentId { get; set; }

    public bool IsPositive { get; set; }
}
