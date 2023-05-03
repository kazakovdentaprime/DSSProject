namespace DSS_Project.Web.DataTransferObjects;

public class CommentDto
{
    public required string Content { get; set; }
    public Guid UserId { get; set; }
}
