using Domain.Entities;
using Domain.Entities.Auth;
using Domain.Entities.Blogs;
using Domain.Entities.Home;
using Domain.Entities.inquiries;
using Domain.Entities.Projects;
using Domain.Entities.Units;
using Domain.Entities.Team;
using Microsoft.EntityFrameworkCore;

namespace infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    public DbSet<Inquiry> Inquiries => Set<Inquiry>();

    public DbSet<Project> Projects => Set<Project>();
    public DbSet<ProjectMedia> ProjectMedia => Set<ProjectMedia>();
    public DbSet<Attraction> Attractions => Set<Attraction>();
    public DbSet<Unit> Units => Set<Unit>();
    public DbSet<UnitMedia> UnitMedia => Set<UnitMedia>();
    public DbSet<BlogCategory> BlogCategories => Set<BlogCategory>();
    public DbSet<Blog> Blogs => Set<Blog>();
    public DbSet<BlogMedia> BlogMedia => Set<BlogMedia>();
    public DbSet<VoiceOfCustomer> VoiceOfCustomers => Set<VoiceOfCustomer>();
    public DbSet<Settings> Settings => Set<Settings>();
    public DbSet<TeamMember> TeamMembers => Set<TeamMember>();
    public DbSet<ProjectDocument> ProjectDocuments => Set<ProjectDocument>();
    public DbSet<UnitDocument> UnitDocuments => Set<UnitDocument>();
    public DbSet<Admin> Admins => Set<Admin>();
    public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}

