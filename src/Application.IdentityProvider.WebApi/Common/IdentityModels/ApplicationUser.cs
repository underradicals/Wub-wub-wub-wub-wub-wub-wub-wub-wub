namespace Application.IdentityProvider.WebApi.IdentityModels;
public class ApplicationUser : IdentityUser<Guid>
{
    
}


public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.ToTable("Users", "Identity");
    }
}