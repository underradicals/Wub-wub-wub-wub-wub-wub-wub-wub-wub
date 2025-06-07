namespace Application.IdentityProvider.WebApi.Common;

public class JwtSettings
{
    public const string SectionName = "JwtSettings";
    public string? Issuer { get; init; }
    public string? Audience { get; init; }
    public string? SecretKey { get; init; }
    public double ExpiryInMinutes { get; init; } // Must be double
    
}