using Domain.Entities;
using Domain.Entities.Units;

namespace Domain.Interfaces;

public interface IUnitRepository
{
    Task<List<Unit>> GetByProjectIdAsync(int projectId, CancellationToken cancellationToken = default);
    Task<Unit?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task AddAsync(Unit unit, CancellationToken cancellationToken = default);
    Task UpdateAsync(Unit unit, CancellationToken cancellationToken = default);
    Task DeleteAsync(Unit unit, CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default);
}