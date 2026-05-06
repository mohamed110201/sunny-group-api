using Application.Common;
using Domain.Entities.Blogs;
using Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Caching.Memory;

namespace Application.Blogs.Commands.CreateBlog;

public class CreateBlogCommandHandler : IRequestHandler<CreateBlogCommand, Result<int>>
{
    private readonly IBlogRepository _blogRepository;
    private readonly IBlogCategoryRepository _categoryRepository;
    private readonly IMemoryCache _cache;

    public CreateBlogCommandHandler(
        IBlogRepository blogRepository,
        IBlogCategoryRepository categoryRepository,
        IMemoryCache cache)
    {
        _blogRepository = blogRepository;
        _categoryRepository = categoryRepository;
        _cache = cache;
    }

    public async Task<Result<int>> Handle(
        CreateBlogCommand request,
        CancellationToken cancellationToken)
    {
        var category = await _categoryRepository.GetByIdAsync(request.CategoryId, cancellationToken);
        if (category == null || !category.IsActive)
            return Result<int>.Failure("Category.NotFound","Category not found or is inactive",404);

        var blog = new Blog
        {
            Title = request.Title,
            Description = request.Description,
            Content = request.Content,
            MetaTitle = request.MetaTitle,
            MetaDescription = request.MetaDescription,
            CategoryId = request.CategoryId,
            CreatedAt = DateTime.UtcNow
        };

        await _blogRepository.AddAsync(blog, cancellationToken);

        _cache.Remove("blogs-all");

        return Result<int>.Success(blog.Id);
    }
}
