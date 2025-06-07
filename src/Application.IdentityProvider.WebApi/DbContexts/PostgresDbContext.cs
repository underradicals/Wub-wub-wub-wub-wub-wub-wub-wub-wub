namespace Application.IdentityProvider.WebApi.DbContexts;

public class PostgresDbContext : 
    IdentityDbContext<
        ApplicationUser,
        ApplicationRole,
        Guid,
        ApplicationUserClaim,
        ApplicationUserRole,
        ApplicationUserLogin,
        ApplicationRoleClaim,
        ApplicationUserToken
    >
{
    public PostgresDbContext(DbContextOptions<PostgresDbContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.HasDefaultSchema("Identity");
        builder.ApplyConfigurationsFromAssembly(typeof(PostgresDbContext).Assembly);
    }
}

public static class PostgresDbContextExtensions
{
    public static void AddPostgresDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<PostgresDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("PostgresConnection"));
            options.EnableDetailedErrors(true);
            options.EnableSensitiveDataLogging();
        });
    }
}