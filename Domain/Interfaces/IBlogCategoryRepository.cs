using Domain.Entities.Blogs;

namespace Domain.Interfaces;

public interface IBlogCategoryRepository
{
    Task<List<BlogCategory>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<BlogCategory?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task AddAsync(BlogCategory category, CancellationToken cancellationToken = default);
    Task UpdateAsync(BlogCategory category, CancellationToken cancellationToken = default);
    Task DeleteAsync(int id, CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default);
    Task<int> GetCategoryBlogCountAsync(int categoryId, CancellationToken cancellationToken = default);
}
