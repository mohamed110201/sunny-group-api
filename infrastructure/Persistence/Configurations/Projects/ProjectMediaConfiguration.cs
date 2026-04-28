using Domain.Entities.Projects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace infrastructure.Persistence.Configurations.Projects;

public class ProjectMediaConfiguration
{
    public void Configure(EntityTypeBuilder<ProjectMedia> builder)
    {
        builder.HasKey(m => m.Id);

        builder.Property(m => m.Url)
            .IsRequired();

        builder.Property(m => m.Category)
            .HasConversion<string>();

        builder.ToTable("ProjectMedia");
    }
}