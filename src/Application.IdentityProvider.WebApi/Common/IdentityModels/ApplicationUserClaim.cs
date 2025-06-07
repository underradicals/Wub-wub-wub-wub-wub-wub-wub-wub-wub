namespace Application.IdentityProvider.WebApi.IdentityModels;
public class ApplicationUserClaim : IdentityUserClaim<Guid>
{
    
}

public class UserClaimConfiguration : IEntityTypeConfiguration<ApplicationUserClaim>
{
    public void Configure(EntityTypeBuilder<ApplicationUserClaim> builder)
    {
        builder.ToTable("UserClaims", "Identity");
    }
}