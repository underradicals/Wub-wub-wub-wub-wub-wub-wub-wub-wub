using Application.IdentityProvider.WebApi.Features.Register;

namespace Application.IdentityProvider.WebApi.Features;

public static class FeatureServicesExtensions
{
    public static IServiceCollection AddEndpointServices(this IServiceCollection services)
    {
        services.AddDemoEndpoint();
        services.AddRegisterUserEndpoint();
        return services;
    }
    
    public static void UseEndpoints(this WebApplication app)
    {
        app.UseDemoEndpoint();
        app.UseRegisterUserEndpoint();
    }
}