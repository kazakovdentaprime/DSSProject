namespace DSS_Project.Web.ViewModels;

public class PostDeleteVM
{
    public Guid Id { get; set; }
    public required string Title { get; set; }
    public required string Body { get; set; }
}
