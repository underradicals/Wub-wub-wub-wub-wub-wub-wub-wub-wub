using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Application.IdentityProvider.WebApi.Features;

public static class DemoEndpoint
{
    public static WebApplication UseDemoEndpoint(this WebApplication app)
    {
        app.MapGet("/hello", Handle).WithName("DemoEndpoint");
        return app;
    }

    public static IServiceCollection AddDemoEndpoint(this IServiceCollection services)
    {
        services.TryAddSingleton<FooBar>();
        return services;
    }

    private static string Handle([FromServices] FooBar foobar)
    {
        return $"Hello World! {foobar.Id}";
    }
}


public class FooBar
{
    public int Id { get; init; } = 5;
}