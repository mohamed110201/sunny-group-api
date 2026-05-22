using Domain.Entities.Home;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace infrastructure.Persistence.Configurations.Home;

public class VoiceOfCustomerConfiguration : IEntityTypeConfiguration<VoiceOfCustomer>
{
    public void Configure(EntityTypeBuilder<VoiceOfCustomer> builder)
    {
        builder.HasKey(v => v.Id);

        builder.Property(v => v.CustomerName)
            .IsRequired()
            .HasMaxLength(100);

        builder.OwnsOne(v => v.Comment, comment =>
        {
            comment.Property(c => c.English).HasColumnName("comment_en").IsRequired();
            comment.Property(c => c.Arabic).HasColumnName("comment_ar");
            comment.Property(c => c.Italian).HasColumnName("comment_it");
            comment.Property(c => c.Russian).HasColumnName("comment_ru");
        });

        builder.HasOne(v => v.Project)
            .WithMany()
            .HasForeignKey(v => v.ProjectId)
            .OnDelete(DeleteBehavior.Cascade);
        builder.Property(v => v.ImageUrl);
        builder.Property(v => v.CreatedAt).IsRequired();


        builder.ToTable("VoiceOfCustomers");
    }
}