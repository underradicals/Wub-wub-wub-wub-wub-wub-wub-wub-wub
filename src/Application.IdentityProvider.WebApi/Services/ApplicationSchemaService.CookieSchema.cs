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
        // options.LoginPath = "/Account/Login";
        // options.AccessDeniedPath = "/Account/AccessDenied";
        // options.LogoutPath = "/Account/Logout";
        options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
        options.SlidingExpiration = true;
        options.Cookie.HttpOnly = true;
        options.Cookie.IsEssential = true;
        options.Cookie.SameSite = SameSiteMode.Lax;
        options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
        options.Cookie.MaxAge = TimeSpan.FromDays(365);
    }
}