using Domain.Entities.Projects;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace infrastructure.Persistence.Configurations.Projects;

public class ProjectConfiguration : IEntityTypeConfiguration<Project>
{
    public void Configure(EntityTypeBuilder<Project> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
            .ValueGeneratedOnAdd();

        builder.OwnsOne(p => p.Name, name =>
        {
            name.Property(n => n.Arabic).HasColumnName("name_ar");
            name.Property(n => n.English).HasColumnName("name_en").IsRequired();
            name.Property(n => n.Italian).HasColumnName("name_it");
            name.Property(n => n.Russian).HasColumnName("name_ru");
        });

        builder.OwnsOne(p => p.Description, desc =>
        {
            desc.Property(d => d.Arabic).HasColumnName("description_ar");
            desc.Property(d => d.English).HasColumnName("description_en").IsRequired();
            desc.Property(d => d.Italian).HasColumnName("description_it");
            desc.Property(d => d.Russian).HasColumnName("description_ru");
        });

        builder.OwnsOne(p => p.PriceRange, price =>
        {
            price.Property(pr => pr.Min).HasColumnName("price_min").HasColumnType("decimal(18,2)");
            price.Property(pr => pr.Max).HasColumnName("price_max").HasColumnType("decimal(18,2)");
        });

        builder.OwnsOne(p => p.Location, loc =>
        {
            loc.Property(l => l.English).HasColumnName("location_en").IsRequired();
            loc.Property(l => l.Arabic).HasColumnName("location_ar");
            loc.Property(l => l.Italian).HasColumnName("location_it");
            loc.Property(l => l.Russian).HasColumnName("location_ru");
        });

        builder.Property(p => p.Area)
            .HasColumnType("double precision");

        builder.Property(p => p.Status)
            .HasConversion<string>();

        var converter = new ValueConverter<List<ProjectType>, string[]>(
            v => v.Select(t => t.ToString()).ToArray(),
            v => v.Select(t => Enum.Parse<ProjectType>(t)).ToList()
        );

        var comparer = new ValueComparer<List<ProjectType>>(
            (c1, c2) => c1!.SequenceEqual(c2!),
            c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
            c => c.ToList()
        );

        builder.Property(p => p.Types)
            .HasColumnType("text[]")
            .HasConversion(converter)
            .Metadata.SetValueComparer(comparer);

        builder.Ignore(p => p.TotalUnits);
        
        builder.HasMany(p => p.Media)
            .WithOne(m => m.Project)
            .HasForeignKey(m => m.ProjectId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasMany(p => p.Attractions)
            .WithOne(m => m.Project)
            .HasForeignKey(m => m.ProjectId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(p => p.Units)
            .WithOne(u => u.Project)
            .HasForeignKey(u => u.ProjectId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.ToTable("Projects");
    }
}