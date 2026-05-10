using Domain.Entities.Blogs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace infrastructure.Persistence.Configurations.Blogs;

public class BlogCategoryConfiguration : IEntityTypeConfiguration<BlogCategory>
{
    public void Configure(EntityTypeBuilder<BlogCategory> builder)
    {
        builder.HasKey(bc => bc.Id);

        builder.Property(bc => bc.Id)
            .ValueGeneratedOnAdd();


        builder.Property(bc => bc.IsActive)
            .IsRequired()
            .HasDefaultValue(true);

        builder.OwnsOne(bc => bc.Name, name =>
        {
            name.Property(n => n.Arabic).HasColumnName("name_ar");
            name.Property(n => n.English).HasColumnName("name_en").IsRequired();
            name.Property(n => n.Italian).HasColumnName("name_it");
            name.Property(n => n.Russian).HasColumnName("name_ru");
        });

        builder.HasMany(bc => bc.Blogs)
            .WithOne(b => b.Category)
            .HasForeignKey(b => b.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.ToTable("BlogCategories");
    }
}
