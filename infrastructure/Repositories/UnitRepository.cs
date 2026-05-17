using Domain.Entities.Units;
using Domain.Interfaces;
using infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class UnitRepository : IUnitRepository
{
    private readonly AppDbContext db_context;

    public UnitRepository(AppDbContext context)
    {
        db_context = context;
    }
    public async Task<List<Unit>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await db_context.Units
            .Include(u => u.Media)
            .Include(u => u.Project)
            .ThenInclude(p => p.Media)
            .ToListAsync(cancellationToken);
    }
    public async Task<List<Unit>> GetByProjectIdAsync(int projectId, CancellationToken cancellationToken = default)
    {
        return await db_context.Units
            .Include(u => u.Media)
            .Include(u=>u.Documents)
            .Include(u=>u.Project)
                  .ThenInclude(p => p.Media)
            .Where(u => u.ProjectId == projectId)
            .ToListAsync(cancellationToken);
    }

    public async Task<Unit?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await db_context.Units
            .Include(u => u.Media)
            .Include(u=>u.Documents)
            .Include(u=>u.Project)
            .ThenInclude(p => p.Media)
            .FirstOrDefaultAsync(u => u.Id == id, cancellationToken);
    }

    public async Task AddAsync(Unit unit, CancellationToken cancellationToken = default)
    {
        await db_context.Units.AddAsync(unit, cancellationToken);
        await db_context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Unit unit, CancellationToken cancellationToken = default)
    {
        db_context.Units.Update(unit);
        await db_context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Unit unit, CancellationToken cancellationToken = default)
    {
        db_context.Units.Remove(unit);
        await db_context.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default)
    {
        return await db_context.Units.AnyAsync(u => u.Id == id, cancellationToken);
    }

    public async Task<int> GetCount(CancellationToken cancellationToken = default)
    {
        return await db_context.Units.CountAsync(cancellationToken);
    }
}