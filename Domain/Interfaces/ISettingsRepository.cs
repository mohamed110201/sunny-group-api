using Domain.Entities;
using Domain.Entities.Home;

namespace Domain.Interfaces;

public interface ISettingsRepository
{
    Task<Settings?> GetAsync(CancellationToken cancellationToken = default);
    Task AddAsync(Settings settings, CancellationToken cancellationToken = default);
    Task UpdateAsync(Settings settings, CancellationToken cancellationToken = default);
}