using Application.IdentityProvider.WebApi.Common;
using Microsoft.EntityFrameworkCore;

namespace Application.IdentityProvider.WebApi.DbContexts;

public class PostgresDbContext : DbContext
{
    public DbSet<ApplicationUser> ApplicationUsers { get; set; }
    
    public PostgresDbContext(DbContextOptions<PostgresDbContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<ApplicationUser>().ToTable("users");
        
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(PostgresDbContext).Assembly);
    }
}

public static class PostgresDbContextExtensions
{
    public static void AddPostgresDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContextPool<PostgresDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("PostgresConnection"));
            options.EnableDetailedErrors(true);
            options.EnableSensitiveDataLogging();
        });
    }
}