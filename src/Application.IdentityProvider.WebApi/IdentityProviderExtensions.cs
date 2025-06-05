using Application.IdentityProvider.WebApi.Common;
using Application.IdentityProvider.WebApi.DbContexts;
using Application.IdentityProvider.WebApi.Features;

namespace Application.IdentityProvider.WebApi;

public static class IdentityProviderExtensions
{
    
    public static WebApplicationBuilder CreateApplicationBuilder(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.AddIdpServices();
        return builder;
    }

    public static WebApplication BuildApplication(this WebApplicationBuilder builder)
    {
        var app = builder.Build();
        app.UseIdpServices();
        return app;
    }
    
    private static WebApplicationBuilder AddIdpServices(this WebApplicationBuilder builder)
    {
        
        builder.Services.RegisterIdpServices(builder);
        return builder;
    }

    private static WebApplication UseIdpServices(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
        }

        app.UseHttpsRedirection();
        app.UseEndpoints();
        return app;
    }

    private static IServiceCollection RegisterIdpServices(this IServiceCollection services, WebApplicationBuilder builder)
    {
        builder.WebHost.ConfigureKestrel(options => { });
        builder.Services.AddProblemDetails();
        builder.Services.AddHttpContextAccessor();
        builder.Services.AddAllSingletons(builder.Configuration);
        builder.Services.AddPostgresDbContext(builder.Configuration);
        builder.Services.AddOpenApi();
        builder.Services.AddEndpointServices();
        return services;
    }
}