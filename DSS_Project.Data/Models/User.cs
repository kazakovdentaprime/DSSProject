namespace DSS_Project.Data.Models;

public class User: BaseModel
{
    public User()
    {
        Posts = new();
        Comments = new();
        CommentRatings = new();
    }

    public required string Email { get; set; }
    public required string Name { get; set; }
    public required string Password { get; set; }

    public virtual List<Post> Posts { get; set; }
    public virtual List<Comment> Comments { get; set; }
    public virtual List<CommentRating> CommentRatings { get; set; }
}
