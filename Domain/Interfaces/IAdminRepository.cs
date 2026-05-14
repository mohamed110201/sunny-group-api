using Domain.Entities;
using Domain.Entities.Auth;

namespace Domain.Interfaces;

public interface IAdminRepository
{
    Task<Admin?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);
    Task<Admin?> GetByRefreshTokenAsync(string token, CancellationToken cancellationToken = default);
    Task UpdateAsync(Admin admin, CancellationToken cancellationToken = default);
}