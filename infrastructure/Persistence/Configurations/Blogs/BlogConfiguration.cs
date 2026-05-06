using Domain.Entities.Blogs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace infrastructure.Persistence.Configurations.Blogs;

public class BlogConfiguration : IEntityTypeConfiguration<Blog>
{
    public void Configure(EntityTypeBuilder<Blog> builder)
    {
        builder.HasKey(b => b.Id);

        builder.Property(b => b.Id)
            .ValueGeneratedOnAdd();

        builder.OwnsOne(b => b.Title, title =>
        {
            title.Property(t => t.English).HasColumnName("title_en").IsRequired();
            title.Property(t => t.Arabic).HasColumnName("title_ar");
            title.Property(t => t.Italian).HasColumnName("title_it");
            title.Property(t => t.Russian).HasColumnName("title_ru");
        });

        builder.OwnsOne(b => b.Description, desc =>
        {
            desc.Property(d => d.English).HasColumnName("description_en").IsRequired();
            desc.Property(d => d.Arabic).HasColumnName("description_ar");
            desc.Property(d => d.Italian).HasColumnName("description_it");
            desc.Property(d => d.Russian).HasColumnName("description_ru");
        });

        builder.OwnsOne(b => b.Content, content =>
        {
            content.Property(c => c.English).HasColumnName("content_en").IsRequired();
            content.Property(c => c.Arabic).HasColumnName("content_ar");
            content.Property(c => c.Italian).HasColumnName("content_it");
            content.Property(c => c.Russian).HasColumnName("content_ru");
        });

        builder.Property(b => b.MetaTitle)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(b => b.MetaDescription)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(b => b.CategoryId)
            .IsRequired();

        builder.HasOne(b => b.Category)
            .WithMany(c => c.Blogs)
            .HasForeignKey(b => b.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Property(b => b.CreatedAt)
            .IsRequired();

        builder.HasMany(b => b.Media)
            .WithOne(m => m.Blog)
            .HasForeignKey(m => m.BlogId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.ToTable("Blogs");
    }
}