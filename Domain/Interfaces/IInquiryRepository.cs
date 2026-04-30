using Domain.Entities;
using Domain.Entities.inquiries;

namespace Domain.Interfaces;

public interface IInquiryRepository
{
    Task<List<Inquiry>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Inquiry?> GetByIdAsync(int id, CancellationToken cancellationToken = default);

    Task AddAsync(Inquiry inquiry, CancellationToken cancellationToken = default);
    Task UpdateAsync(Inquiry inquiry, CancellationToken cancellationToken = default);
}