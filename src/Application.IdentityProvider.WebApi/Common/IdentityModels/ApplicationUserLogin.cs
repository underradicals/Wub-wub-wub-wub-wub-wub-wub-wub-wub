namespace Application.IdentityProvider.WebApi.IdentityModels;
public class ApplicationUserLogin : IdentityUserLogin<Guid>
{
    
}


public class ApplicationUserLoginConfiguration : IEntityTypeConfiguration<ApplicationUserLogin>
{
    public void Configure(EntityTypeBuilder<ApplicationUserLogin> builder)
    {
        builder.ToTable("UserLogins", "Identity");
    }
}