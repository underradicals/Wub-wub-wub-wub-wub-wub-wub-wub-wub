namespace Application.IdentityProvider.WebApi.IdentityModels;
public class ApplicationRole : IdentityRole<Guid>
{
    
}

public class ApplicationRoleConfiguration : IEntityTypeConfiguration<ApplicationRole>
{
    public void Configure(EntityTypeBuilder<ApplicationRole> builder)
    {
        builder.ToTable("Roles", "Identity");
    }
}