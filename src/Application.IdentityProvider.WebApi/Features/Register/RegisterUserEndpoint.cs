using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace Application.IdentityProvider.WebApi.Features.Register;

public static class RegisterUserEndpoint
{
    public static IServiceCollection AddRegisterUserEndpoint(this IServiceCollection services)
    {
        services.AddTransient<IValidator<RegisterUserRequest>, RegisterUserValidator>();
        services.AddTransient<IRegisterUserRepository, RegisterUserRepository>();
        services.AddTransient<IRequestHandler<RegisterUserRequest, IResult>, RegisterUserHandler>();
        return services;
    }


    public static WebApplication UseRegisterUserEndpoint(this WebApplication app)
    {
        app.MapPost("/register", Handle).WithName("RegisterUser");
        return app;
    }


    private static async Task<IResult> Handle([FromServices] IRequestHandler<RegisterUserRequest, IResult> handler, RegisterUserRequest request, CancellationToken token)
    {
        return await handler.Handle(request, token);
    }
}