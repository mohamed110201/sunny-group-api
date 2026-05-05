using Domain.Entities.Blogs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace infrastructure.Persistence.Configurations.Blogs;

public class BlogMediaConfiguration : IEntityTypeConfiguration<BlogMedia>
{
    public void Configure(EntityTypeBuilder<BlogMedia> builder)
    {
        builder.HasKey(m => m.Id);

        builder.Property(m => m.Url)
            .IsRequired();

        builder.Property(m => m.Category)
            .HasConversion<string>();

        builder.ToTable("BlogMedia");
    }
}