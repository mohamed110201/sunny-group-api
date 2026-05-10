using Domain.Entities.inquiries;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace infrastructure.Persistence.Configurations.inquiries;

public class InquiryConfiguration : IEntityTypeConfiguration<Inquiry>
{
    public void Configure(EntityTypeBuilder<Inquiry> builder)
    {
        builder.HasKey(i => i.Id);

        builder.Property(i => i.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(i => i.CountryCode)
            .IsRequired()
            .HasMaxLength(10);

        builder.Property(i => i.PhoneNumber)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(i => i.Email)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(i => i.Message)
            .IsRequired()
            .HasMaxLength(1000);

        builder.Property(i => i.IsSeen)
            .IsRequired()
            .HasDefaultValue(false);

        builder.Property(i => i.CreatedAt)
            .IsRequired();
        builder.Property(i => i.PageSlug)
            .HasMaxLength(500);

        builder.Ignore(i => i.FullPhoneNumber);

        builder.ToTable("Inquiries");
    }
}