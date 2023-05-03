using DSS_Project.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace DSS_Project.Data;

public class ApplicationDbContext: DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
      : base(options)
    {
    }

    public ApplicationDbContext()
    {
    }

    public DbSet<Comment> Comments { get; set; }
    public DbSet<CommentRating> CommentRatings { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<User> Users { get; set; }
}