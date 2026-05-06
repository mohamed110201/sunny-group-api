using Domain.Entities.Blogs;
using Domain.Interfaces;
using infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class BlogCategoryRepository : IBlogCategoryRepository
{
    private readonly AppDbContext _context;

    public BlogCategoryRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<BlogCategory>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.BlogCategories
            .Where(bc => bc.IsActive)
            .ToListAsync(cancellationToken);
    }

    public async Task<BlogCategory?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _context.BlogCategories
            .FirstOrDefaultAsync(bc => bc.Id == id, cancellationToken);
    }

    public async Task AddAsync(BlogCategory category, CancellationToken cancellationToken = default)
    {
        await _context.BlogCategories.AddAsync(category, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(BlogCategory category, CancellationToken cancellationToken = default)
    {
        _context.BlogCategories.Update(category);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var category = await GetByIdAsync(id, cancellationToken);
        if (category != null)
        {
            _context.BlogCategories.Remove(category);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }

    public async Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _context.BlogCategories.AnyAsync(bc => bc.Id == id, cancellationToken);
    }

    public async Task<int> GetCategoryBlogCountAsync(int categoryId, CancellationToken cancellationToken = default)
    {
        return await _context.Blogs.CountAsync(b => b.CategoryId == categoryId, cancellationToken);
    }
}
