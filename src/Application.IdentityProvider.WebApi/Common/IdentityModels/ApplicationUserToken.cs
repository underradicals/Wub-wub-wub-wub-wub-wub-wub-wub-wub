namespace Application.IdentityProvider.WebApi.IdentityModels;
public class ApplicationUserToken : IdentityUserToken<Guid>
{
    
}

public class ApplicationUserTokenConfiguration : IEntityTypeConfiguration<ApplicationUserToken>
{
    public void Configure(EntityTypeBuilder<ApplicationUserToken> builder)
    {
        builder.ToTable("UserTokens", "Identity");
    }
}