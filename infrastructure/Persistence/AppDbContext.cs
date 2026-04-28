using Domain.Entities.Projects;
using Domain.Entities.Units;
using Microsoft.EntityFrameworkCore;

namespace infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Project> Projects => Set<Project>();
    public DbSet<ProjectMedia> ProjectMedia => Set<ProjectMedia>();
    public DbSet<Attraction> Attractions => Set<Attraction>();
    public DbSet<Unit> Units => Set<Unit>();
    public DbSet<UnitMedia> UnitMedia => Set<UnitMedia>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}

