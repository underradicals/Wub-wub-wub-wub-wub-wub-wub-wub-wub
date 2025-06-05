using Application.IdentityProvider.WebApi.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Application.IdentityProvider.WebApi.Features.Register;

public class ApplicationUserEntityConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .HasColumnName("id").IsRequired();
        
        builder.Property<uint>("xmin").HasColumnName("xmin").IsRowVersion();
        
        builder.Property(x => x.Email).HasColumnName("email").HasMaxLength(256);
        builder.Property(x => x.Password).HasColumnName("password").HasMaxLength(256);
        builder.Property(x => x.EmailConfirmed).HasColumnName("email_confirmed");
        builder.Property(x => x.NormalizedEmail).HasColumnName("normalized_email").HasMaxLength(256);
        builder.Property(x => x.Created).HasColumnName("created");
        builder.Property(x => x.Modified).HasColumnName("modified");
        builder.Property(x => x.RefreshToken).HasColumnName("refresh_token");
        builder.Property(x => x.SecurityStamp).HasColumnName("secret");
        builder.Property(x => x.LastLogin).HasColumnName("last_login");
        
        builder.HasIndex(x => x.Email).IsUnique();
        builder.HasIndex(x => x.NormalizedEmail).IsUnique();
        builder.HasIndex(x => x.Created);
        builder.HasIndex(x => x.Modified);
        builder.HasIndex(x => x.LastLogin).IsUnique();
        builder.HasIndex(x => x.RefreshToken).IsUnique();
    }
}