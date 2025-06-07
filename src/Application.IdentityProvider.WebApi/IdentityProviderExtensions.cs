using Microsoft.AspNetCore.Authorization;

namespace Application.IdentityProvider.WebApi;

public static class IdentityProviderExtensions
{
    
    public static WebApplicationBuilder CreateApplication(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        
        builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection(JwtSettings.SectionName));
        builder.Services.AddSingleton<JwtSettings>(serviceProvider => serviceProvider.GetRequiredService<IOptions<JwtSettings>>().Value);

        builder.Services.AddIdpIdentity(builder.Configuration);
        builder.AddIdpServices();
        return builder;
    }

    public static WebApplication BuildApplication(this WebApplicationBuilder builder)
    {
        var app = builder.Build();
        app.UseIdpServices();
        
        app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapIdentityApi<ApplicationUser>();
        
        app.MapGet("/secure", [Authorize] () => "Secure")
            .RequireAuthorization();
        
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