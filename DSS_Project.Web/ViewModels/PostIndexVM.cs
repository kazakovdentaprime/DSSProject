using DSS_Project.Data.Models;

namespace DSS_Project.Web.ViewModels;

public class PostIndexVM
{
	public PostIndexVM()
	{
		Posts = new();
	}

    public List<Post> Posts { get; set; }
}
