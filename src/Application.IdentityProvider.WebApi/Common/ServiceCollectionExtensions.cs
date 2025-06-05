using Application.IdentityProvider.WebApi.Common.EmailVerification;

namespace Application.IdentityProvider.WebApi.Common;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAllSingletons(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IEmailVerificationService, EmailVerificationService>();
        return services;
    }
}