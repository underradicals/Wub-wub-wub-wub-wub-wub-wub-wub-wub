namespace Application.IdentityProvider.WebApi.IdentityModels;
public class ApplicationRoleClaim : IdentityRoleClaim<Guid>
{
    
}

public class ApplicationRoleClaimConfiguration : IEntityTypeConfiguration<ApplicationRoleClaim>
{
    public void Configure(EntityTypeBuilder<ApplicationRoleClaim> builder)
    {
        builder.ToTable("RoleClaims", "Identity");
    }
}