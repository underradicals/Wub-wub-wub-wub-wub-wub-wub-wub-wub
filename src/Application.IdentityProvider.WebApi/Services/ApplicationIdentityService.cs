using Application.IdentityProvider.WebApi.DbContexts;

namespace Application.IdentityProvider.WebApi.Services;


public static partial class ApplicationSchemaService
{
    public static AuthenticationBuilder AddIdpSchemas(this AuthenticationBuilder services, IConfiguration configuration)
    {
        services
            .AddIdpCookieSchema(configuration)
            .AddIdpJsonWebTokenSchema(configuration);
        return services;
    }
}

public static class ApplicationIdentityService
{
    public static IServiceCollection AddIdpIdentity(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(AddAuthenticationOptions)
            .AddIdpSchemas(configuration);
        services.AddIdentityCore<ApplicationUser>()
            .AddEntityFrameworkStores<PostgresDbContext>()
            .AddApiEndpoints();
        services.AddDbContext<PostgresDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("PostgresConnection"));
            options.EnableDetailedErrors(true);
            options.EnableSensitiveDataLogging();
        });
        return services;
    }

    private static void AddAuthenticationOptions(AuthenticationOptions options)
    {
        options.DefaultScheme = IdentityConstants.BearerScheme;
        options.DefaultChallengeScheme = IdentityConstants.BearerScheme;
        options.DefaultAuthenticateScheme = IdentityConstants.BearerScheme;
    }
}

