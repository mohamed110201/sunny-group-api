using Domain.Entities;
using Domain.Entities.inquiries;
using Domain.Interfaces;
using infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class InquiryRepository : IInquiryRepository
{
    private readonly AppDbContext _context;

    public InquiryRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Inquiry>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Inquiries
            .OrderByDescending(i => i.CreatedAt)
            .ToListAsync(cancellationToken);
    }
    public async Task<Inquiry?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _context.Inquiries
            .FirstOrDefaultAsync(i => i.Id == id, cancellationToken);
    }
    public async Task AddAsync(Inquiry inquiry, CancellationToken cancellationToken = default)
    {
        await _context.Inquiries.AddAsync(inquiry, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Inquiry inquiry, CancellationToken cancellationToken = default)
    {
        _context.Inquiries.Update(inquiry);
        await _context.SaveChangesAsync(cancellationToken);
    }
}