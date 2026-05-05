using Domain.Entities;
using Domain.Entities.Home;

namespace Domain.Interfaces;

public interface IVoiceOfCustomerRepository
{
    Task<List<VoiceOfCustomer>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<VoiceOfCustomer?> GetByIdAsync(int id, CancellationToken cancellationToken = default);

    Task AddAsync(VoiceOfCustomer voiceOfCustomer, CancellationToken cancellationToken = default);
    Task UpdateAsync(VoiceOfCustomer voiceOfCustomer, CancellationToken cancellationToken = default);
    Task DeleteAsync(VoiceOfCustomer voiceOfCustomer, CancellationToken cancellationToken = default);
}