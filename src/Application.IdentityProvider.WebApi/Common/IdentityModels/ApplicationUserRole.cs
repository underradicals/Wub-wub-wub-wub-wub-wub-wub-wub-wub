namespace Application.IdentityProvider.WebApi.IdentityModels;
public class ApplicationUserRole : IdentityUserRole<Guid>
{
    
}

public class ApplicationUserRoleConfiguration : IEntityTypeConfiguration<ApplicationUserRole>
{
    public void Configure(EntityTypeBuilder<ApplicationUserRole> builder)
    {
        builder.ToTable("UserRoles", "Identity");
    }
}