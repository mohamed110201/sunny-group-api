using Domain.Entities.Team;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace infrastructure.Persistence.Configurations.Team;

public class TeamMemberConfiguration : IEntityTypeConfiguration<TeamMember>
{
    public void Configure(EntityTypeBuilder<TeamMember> builder)
    {
        builder.HasKey(t => t.Id);

        builder.OwnsOne(t => t.Name, name =>
        {
            name.Property(n => n.English).HasColumnName("name_en").HasMaxLength(150).IsRequired();
            name.Property(n => n.Arabic).HasColumnName("name_ar").HasMaxLength(150);
            name.Property(n => n.Italian).HasColumnName("name_it").HasMaxLength(150);
            name.Property(n => n.Russian).HasColumnName("name_ru").HasMaxLength(150);
        });

        builder.OwnsOne(t => t.Position, position =>
        {
            position.Property(p => p.English).HasColumnName("position_en").HasMaxLength(100).IsRequired();
            position.Property(p => p.Arabic).HasColumnName("position_ar").HasMaxLength(100);
            position.Property(p => p.Italian).HasColumnName("position_it").HasMaxLength(100);
            position.Property(p => p.Russian).HasColumnName("position_ru").HasMaxLength(100);
        });

        builder.OwnsOne(t => t.Department, department =>
        {
            department.Property(d => d.English).HasColumnName("department_en").HasMaxLength(100).IsRequired();
            department.Property(d => d.Arabic).HasColumnName("department_ar").HasMaxLength(100);
            department.Property(d => d.Italian).HasColumnName("department_it").HasMaxLength(100);
            department.Property(d => d.Russian).HasColumnName("department_ru").HasMaxLength(100);
        });

        builder.Property(t => t.PhoneNumber).IsRequired().HasMaxLength(20);
        builder.Property(t => t.Email).HasMaxLength(200);
        builder.Property(t => t.ImageUrl).HasMaxLength(1000);

        builder.OwnsOne(t => t.Links, links =>
        {
            links.Property(l => l.Facebook).HasColumnName("facebook").HasMaxLength(500);
            links.Property(l => l.Instagram).HasColumnName("instagram").HasMaxLength(500);
            links.Property(l => l.Youtube).HasColumnName("youtube").HasMaxLength(500);
            links.Property(l => l.LinkedIn).HasColumnName("linkedin").HasMaxLength(500);
            links.Property(l => l.Other).HasColumnName("other").HasMaxLength(1000);
        });

        builder.ToTable("TeamMembers");
    }
}
