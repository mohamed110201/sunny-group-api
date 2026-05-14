using Domain.Entities;
using Domain.Entities.Auth;
using Domain.Interfaces;
using infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class AdminRepository : IAdminRepository
{
    private readonly AppDbContext _context;

    public AdminRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Admin?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        return await _context.Admins
            .Include(a => a.RefreshTokens)
            .FirstOrDefaultAsync(a => a.Email == email, cancellationToken);
    }

    public async Task<Admin?> GetByRefreshTokenAsync(string token, CancellationToken cancellationToken = default)
    {
        return await _context.Admins
            .Include(a => a.RefreshTokens)
            .FirstOrDefaultAsync(a => a.RefreshTokens
                .Any(r => r.Token == token), cancellationToken);
    }

    public async Task UpdateAsync(Admin admin, CancellationToken cancellationToken = default)
    {
        _context.Admins.Update(admin);
        await _context.SaveChangesAsync(cancellationToken);
    }
}