namespace DSS_Project.Data.Models;

public class Post: BaseModel
{
    public Post()
    {
        Comments = new();
    }

    public required string Title { get; set; }
    public required string Body { get; set; }

    public virtual User? User { get; set; }
    public required Guid UserId { get; set; }
    
    public virtual List<Comment> Comments { get; set; }
}
