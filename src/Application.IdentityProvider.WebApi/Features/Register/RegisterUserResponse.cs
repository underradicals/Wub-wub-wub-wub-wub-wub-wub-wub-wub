namespace Application.IdentityProvider.WebApi.Features.Register;

internal record RegisterUserResponse
{
    public Guid Id { get; init; }
    public string Email { get; init; } = string.Empty;
    public bool IsSuccessful { get; init; }
}