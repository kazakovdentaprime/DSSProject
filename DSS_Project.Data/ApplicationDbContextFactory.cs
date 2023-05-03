using DSS_Project.Data;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
        optionsBuilder.UseNpgsql("Host=localhost;Database=DSS;Username=postgres;Password=postgre;Include Error Detail=true");

        return new ApplicationDbContext(optionsBuilder.Options);
    }
}