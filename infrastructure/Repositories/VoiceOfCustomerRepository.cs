using Domain.Entities;
using Domain.Entities.Home;
using Domain.Interfaces;
using infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class VoiceOfCustomerRepository : IVoiceOfCustomerRepository
{
    private readonly AppDbContext _context;

    public VoiceOfCustomerRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<VoiceOfCustomer>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.VoiceOfCustomers
            .Include(v => v.Project)
            .OrderByDescending(v => v.CreatedAt)
            .ToListAsync(cancellationToken);
    }

    public async Task<VoiceOfCustomer?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _context.VoiceOfCustomers
            .Include(v => v.Project)
            .FirstOrDefaultAsync(v => v.Id == id, cancellationToken);
    }


    public async Task AddAsync(VoiceOfCustomer voiceOfCustomer, CancellationToken cancellationToken = default)
    {
        await _context.VoiceOfCustomers.AddAsync(voiceOfCustomer, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(VoiceOfCustomer voiceOfCustomer, CancellationToken cancellationToken = default)
    {
        _context.VoiceOfCustomers.Update(voiceOfCustomer);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(VoiceOfCustomer voiceOfCustomer, CancellationToken cancellationToken = default)
    {
        _context.VoiceOfCustomers.Remove(voiceOfCustomer);
        await _context.SaveChangesAsync(cancellationToken);
    }
}