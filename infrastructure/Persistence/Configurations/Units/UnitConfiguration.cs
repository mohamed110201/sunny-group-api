using Domain.Entities.Units;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace infrastructure.Persistence.Configurations.Units;

public class UnitConfiguration : IEntityTypeConfiguration<Unit>
{
    public void Configure(EntityTypeBuilder<Unit> builder)
    {
        builder.HasKey(u => u.Id);

        builder.Property(u => u.Id)
            .ValueGeneratedOnAdd();

        builder.OwnsOne(u => u.Name, name =>
        {
            name.Property(n => n.English).HasColumnName("name_en").IsRequired();
            name.Property(n => n.Arabic).HasColumnName("name_ar");
            name.Property(n => n.Italian).HasColumnName("name_it");
            name.Property(n => n.Russian).HasColumnName("name_ru");
        });

        builder.OwnsOne(u => u.PaymentPlan, plan =>
        {
            plan.Property(p => p.English).HasColumnName("payment_plan_en").IsRequired();
            plan.Property(p => p.Arabic).HasColumnName("payment_plan_ar");
            plan.Property(p => p.Italian).HasColumnName("payment_plan_it");
            plan.Property(p => p.Russian).HasColumnName("payment_plan_ru");
        });

        builder.OwnsOne(u => u.MetaTitle, meta =>
        {
            meta.Property(m => m.English).HasColumnName("meta_title_en").IsRequired();
            meta.Property(m => m.Arabic).HasColumnName("meta_title_ar");
            meta.Property(m => m.Italian).HasColumnName("meta_title_it");
            meta.Property(m => m.Russian).HasColumnName("meta_title_ru");
        });

        builder.OwnsOne(u => u.MetaDescription, meta =>
        {
            meta.Property(m => m.English).HasColumnName("meta_description_en");
            meta.Property(m => m.Arabic).HasColumnName("meta_description_ar");
            meta.Property(m => m.Italian).HasColumnName("meta_description_it");
            meta.Property(m => m.Russian).HasColumnName("meta_description_ru");
        });

        builder.Property(u => u.Area)
            .IsRequired();

        builder.Property(u => u.Rooms)
            .IsRequired();

        builder.Property(u => u.Status)
            .HasConversion<string>();

        builder.Property(u => u.Type)
            .HasConversion<string>();
        builder.Property(u => u.DeliveryCondition)
            .HasConversion<string>();

        builder.Property(u => u.Price)
            .HasColumnType("decimal(18,2)");

        builder.Property(u => u.Stock)
            .IsRequired();

        builder.Property(u => u.ROI)
            .HasColumnType("double precision");

        builder.HasMany(u => u.Media)
            .WithOne(m => m.Unit)
            .HasForeignKey(m => m.UnitId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.ToTable("Units");
    }
}