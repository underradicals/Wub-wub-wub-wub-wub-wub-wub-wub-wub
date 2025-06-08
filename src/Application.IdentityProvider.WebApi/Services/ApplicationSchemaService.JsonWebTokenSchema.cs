namespace Application.IdentityProvider.WebApi.Services;

public static partial class ApplicationSchemaService
{
    private static AuthenticationBuilder AddIdpJsonWebTokenSchema(this AuthenticationBuilder services,
        IConfiguration configuration)
    {
        services.AddBearerToken(IdentityConstants.BearerScheme, options =>
        {
            options.BearerTokenExpiration = TimeSpan.FromHours(1);
            options.RefreshTokenExpiration = TimeSpan.FromDays(7);
            options.Validate();
        });
        return services;
    }

    private static Action<JwtBearerOptions> JsonWebTokenSchemaConfiguration(IConfiguration configuration)
    {
        return (JwtBearerOptions options) =>
        {
            options.Audience = configuration.GetSection(JwtSettings.SectionName)["Audience"];
            options.Authority = configuration.GetSection(JwtSettings.SectionName)["Issuer"];
            options.RequireHttpsMetadata = false;
            options.SaveToken = true;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                IssuerSigningKey =
                    new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(configuration.GetSection(JwtSettings.SectionName)["SecretKey"]!)),
                ValidIssuer = configuration.GetSection(JwtSettings.SectionName)["Issuer"]!,
                ValidAudience = configuration.GetSection(JwtSettings.SectionName)["Audience"]!,
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidateIssuerSigningKey = true,
                ValidateLifetime = true,
                ValidAlgorithms = [SecurityAlgorithms.HmacSha256, SecurityAlgorithms.RsaSha256],
            };
        };
    }
}