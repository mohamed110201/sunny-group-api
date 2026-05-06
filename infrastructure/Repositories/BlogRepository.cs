using Domain.Entities;
using Domain.Entities.Blogs;
using Domain.Interfaces;
using infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class BlogRepository : IBlogRepository
{
    private readonly AppDbContext _context;

    public BlogRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Blog>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Blogs
            .Include(b => b.Media)
            .Include(b => b.Category)
            .OrderByDescending(b => b.CreatedAt)
            .ToListAsync(cancellationToken);
    }

    public async Task<Blog?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _context.Blogs
            .Include(b => b.Media)
            .Include(b => b.Category)
            .FirstOrDefaultAsync(b => b.Id == id, cancellationToken);
    }

    public async Task AddAsync(Blog blog, CancellationToken cancellationToken = default)
    {
        await _context.Blogs.AddAsync(blog, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Blog blog, CancellationToken cancellationToken = default)
    {
        _context.Blogs.Update(blog);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Blog blog, CancellationToken cancellationToken = default)
    {
        _context.Blogs.Remove(blog);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _context.Blogs.AnyAsync(b => b.Id == id, cancellationToken);
    }
}