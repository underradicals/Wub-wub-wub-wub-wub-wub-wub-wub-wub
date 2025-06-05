using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Application.IdentityProvider.WebApi.Common;

public class ApplicationUser
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public string Email { get; init; } = string.Empty;
    public string NormalizedEmail { get; init; } = string.Empty;
    public string Password { get; init; } = string.Empty;
    public DateTime Created { get; init; } = DateTime.UtcNow;
    public DateTime Modified { get; init; } = DateTime.UtcNow;
    public bool EmailConfirmed { get; init; } = false;
    public string? RefreshToken { get; init; }
    public string SecurityStamp { get; init; } = Guid.NewGuid().ToString();
    public DateTime? LastLogin { get; init; }
    [ConcurrencyCheck]
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public uint xmin { get; private set; }
}