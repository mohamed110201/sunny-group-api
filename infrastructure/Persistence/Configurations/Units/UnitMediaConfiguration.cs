using Domain.Entities.Units;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace infrastructure.Persistence.Configurations.Units;

public class UnitMediaConfiguration: IEntityTypeConfiguration<UnitMedia>
{
    public void Configure(EntityTypeBuilder<UnitMedia> builder)
    {
        builder.HasKey(m => m.Id);

        builder.Property(m => m.Url)
            .IsRequired();

        builder.Property(m => m.Category)
            .HasConversion<string>();

        builder.Property(m => m.SubCategory)
            .HasConversion<string>();

        builder.ToTable("UnitMedia");
    }
}
