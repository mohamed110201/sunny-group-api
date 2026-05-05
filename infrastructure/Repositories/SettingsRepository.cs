using Domain.Entities;
using Domain.Entities.Home;
using Domain.Interfaces;
using infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class SettingsRepository : ISettingsRepository
{
    private readonly AppDbContext _context;

    public SettingsRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Settings?> GetAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Settings
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task AddAsync(Settings settings, CancellationToken cancellationToken = default)
    {
        await _context.Settings.AddAsync(settings, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Settings settings, CancellationToken cancellationToken = default)
    {
        _context.Settings.Update(settings);
        await _context.SaveChangesAsync(cancellationToken);
    }
}