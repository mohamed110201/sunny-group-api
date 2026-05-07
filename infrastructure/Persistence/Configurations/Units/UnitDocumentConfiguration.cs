using Domain.Entities.Units;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace infrastructure.Persistence.Configurations.Units;

public class UnitDocumentConfiguration : IEntityTypeConfiguration<UnitDocument>
{
    public void Configure(EntityTypeBuilder<UnitDocument> builder)
    {
        builder.HasKey(d => d.Id);
        builder.Property(d => d.Url).IsRequired();
        builder.Property(d => d.Category).HasConversion<string>();

        builder.HasOne(d => d.Unit)
            .WithMany(u => u.Documents)
            .HasForeignKey(d => d.UnitId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.ToTable("UnitDocuments");
    }
}