namespace DSS_Project.Data.Models;

public class Comment: BaseModel
{
    public Comment()
    {
        CommentRatings = new();
    }

    public required string Content { get; set; }

    public Guid PostId { get; set; }
    public Post? Post { get; set; }

    public Guid UserId { get; set; }
    public User? User { get; set; }

    public virtual List<CommentRating> CommentRatings { get; set; }
}
