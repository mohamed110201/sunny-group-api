using Domain.Entities;
using Domain.Entities.Blogs;

namespace Domain.Interfaces;

public interface IBlogRepository
{
    Task<List<Blog>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Blog?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task AddAsync(Blog blog, CancellationToken cancellationToken = default);
    Task UpdateAsync(Blog blog, CancellationToken cancellationToken = default);
    Task DeleteAsync(Blog blog, CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default);
}