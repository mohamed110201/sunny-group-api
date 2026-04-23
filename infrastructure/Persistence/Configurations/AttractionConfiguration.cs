using Domain.Entities.Projects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace infrastructure.Persistence.Configurations;

public class AttractionConfiguration : IEntityTypeConfiguration<Attraction>
{
    public void Configure(EntityTypeBuilder<Attraction> builder)
    {
        builder.HasKey(a => a.Id);

        builder.OwnsOne(a => a.Name, name =>
        {
            name.Property(n => n.Arabic).HasColumnName("name_ar");
            name.Property(n => n.English).HasColumnName("name_en").IsRequired();
            name.Property(n => n.Italian).HasColumnName("name_it");
            name.Property(n => n.Russian).HasColumnName("name_ru");
        });

        builder.Property(a => a.TimeInMinutes)
            .IsRequired();

        builder.Property(a => a.IconUrl);
            

        builder.ToTable("Attractions");
    }
}