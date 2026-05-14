using Domain.Entities.Auth;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace infrastructure.Persistence.Configurations.Auth;

public class AdminConfiguration : IEntityTypeConfiguration<Admin>
{
    public void Configure(EntityTypeBuilder<Admin> builder)
    {
        builder.HasKey(a => a.Id);

        builder.Property(a => a.Email)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(a => a.PasswordHash)
            .IsRequired();

        builder.HasMany(a => a.RefreshTokens)
            .WithOne(r => r.Admin)
            .HasForeignKey(r => r.AdminId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.ToTable("Admins");
    }
}