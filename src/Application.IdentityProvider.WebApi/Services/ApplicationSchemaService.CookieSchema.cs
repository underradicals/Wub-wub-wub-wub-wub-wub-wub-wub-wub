namespace Application.IdentityProvider.WebApi.Services;

public static partial class ApplicationSchemaService
{
    private static AuthenticationBuilder AddIdpCookieSchema(this AuthenticationBuilder services, IConfiguration configuration)
    {
        services.AddCookie(IdentityConstants.ApplicationScheme, CookieSchemaConfiguration);
        return services;
    }

    private static void CookieSchemaConfiguration(CookieAuthenticationOptions options)
    {
        options.Cookie.Name = "Identity";
        options.Cookie.HttpOnly = true;
        options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
        options.Cookie.SameSite = SameSiteMode.Lax;
        options.Cookie.IsEssential = true;
    }
}