namespace Application.IdentityProvider.WebApi.Services;

public class TokenService
{
    private readonly JwtSettings _settings;

    public TokenService(JwtSettings settings)
    {
        _settings = settings;
    }

    private static string GenerateSecureRefreshToken(int size = 64)
    {
        var randomBytes = new byte[size];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomBytes);
        return Convert.ToBase64String(randomBytes);
    }

    public JwtToken GenerateToken(Guid userId, string email)
    {
        // Create Key
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.SecretKey!));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        
        // Create Claims
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Email, email),
            new Claim(JwtRegisteredClaimNames.Iss, _settings.Issuer!),
            new Claim(JwtRegisteredClaimNames.Aud, _settings.Audience!),
        };
        
        
        // Create Token
        var token = new JwtSecurityToken(
            issuer: _settings.Issuer!,
            audience: _settings.Audience!,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_settings.ExpiryInMinutes),
            notBefore: DateTime.UtcNow,
            signingCredentials: credentials
        );
        
        // Write Token
        return new JwtToken
        {
            Token = new JwtSecurityTokenHandler().WriteToken(token),
            Expiration = DateTime.UtcNow.AddMinutes(_settings.ExpiryInMinutes),
            RefreshToken = GenerateSecureRefreshToken(),
        };
    }
}

public record JwtToken
{
    public string Token { get; init; } = null!;
    public DateTime Expiration { get; init; }
    public string RefreshToken { get; init; } = null!;
}