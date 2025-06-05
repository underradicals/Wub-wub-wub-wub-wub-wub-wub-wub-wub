namespace Application.IdentityProvider.WebApi.Features.Register;

internal class RegisterUserRequest
{
    public string Password { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}