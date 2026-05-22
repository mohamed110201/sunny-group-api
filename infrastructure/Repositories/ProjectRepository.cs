using Domain.Entities.Projects;
using Domain.Interfaces;
using Domain.ValueObjects;
using infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace infrastructure.Repositories;

public class ProjectRepository : IProjectRepository
{
    private readonly AppDbContext db_context;

    public ProjectRepository(AppDbContext _context)
    {
        db_context = _context;
    }

    public async Task<List<Project>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await db_context.Projects
            .Include(p => p.Media)
            .OrderByDescending(p => p.CreatedAt)
            .ToListAsync(cancellationToken);
    }

    public async Task<Project?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await db_context.Projects
            .Include(p => p.Media)
            .Include(p => p.Documents)
            .Include(p => p.Units)
            .Include(p => p.Attractions)
            .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
    }

    public async Task AddAsync(Project project, CancellationToken cancellationToken = default)
    {
        await db_context.Projects.AddAsync(project, cancellationToken);
        await db_context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Project project, CancellationToken cancellationToken = default)
    {
        db_context.Projects.Update(project);
        await db_context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Project project, CancellationToken cancellationToken = default)
    {
        db_context.Projects.Remove(project);
        await db_context.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default)
    {
        return await db_context.Projects.AnyAsync(p => p.Id == id, cancellationToken);
    }

    public async Task<int> GetCount(CancellationToken cancellationToken = default)
    {
        return await db_context.Projects.CountAsync(cancellationToken);
    }

    public async Task UpdatePriceRangeAsync(int projectId, CancellationToken cancellationToken = default)
    {
        var units = await db_context.Units
            .Where(u => u.ProjectId == projectId)
            .Select(u => u.Price)
            .ToListAsync(cancellationToken);

        var project = await db_context.Projects.FindAsync([projectId], cancellationToken);
        if (project != null)
        {
            if (units.Any())
            {
                var minPrice = units.Min();
                var maxPrice = units.Max();

                project.PriceRange = new PriceRange
                {
                    Min = minPrice,
                    Max = maxPrice
                };
            }
            else
            {
                project.PriceRange = null;
            }
            await db_context.SaveChangesAsync(cancellationToken);
        }
    }
}