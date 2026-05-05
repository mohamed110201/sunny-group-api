using Domain.Entities.Home;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace infrastructure.Persistence.Configurations.Home;

public class SettingsConfiguration : IEntityTypeConfiguration<Settings>
{
    public void Configure(EntityTypeBuilder<Settings> builder)
    {
        builder.HasKey(s => s.Id);

        builder.Property(s => s.CompanyFoundingYear)
            .IsRequired();

        builder.Property(s => s.NumberOfClients)
            .IsRequired();

        builder.Property(s => s.HomeMediaUrl);

        builder.ToTable("Settings");
    }
}