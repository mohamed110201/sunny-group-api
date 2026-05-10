using Application.Common;
using Application.Common.Interfaces;
using Domain.Entities.Blogs;
using Domain.Interfaces;
using Domain.ValueObjects;
using MediatR;
using Microsoft.Extensions.Caching.Memory;

namespace Application.Blogs.Commands.CreateBlog;

public class CreateBlogCommandHandler : IRequestHandler<CreateBlogCommand, Result<int>>
{
    private readonly IBlogRepository _blogRepository;
    private readonly IBlogCategoryRepository _categoryRepository;
    private readonly IMemoryCache _cache;
    private readonly IHtmlSanitizerService _sanitizer;


    public CreateBlogCommandHandler(
        IBlogRepository blogRepository,
        IBlogCategoryRepository categoryRepository,
        IMemoryCache cache,
        IHtmlSanitizerService sanitizer)
    {
        _blogRepository = blogRepository;
        _categoryRepository = categoryRepository;
        _cache = cache;
        _sanitizer = sanitizer;
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
            Content = new LocalizedText
            {
                English = _sanitizer.Sanitize(request.Content.English),
                Arabic = request.Content.Arabic is null ? null : _sanitizer.Sanitize(request.Content.Arabic),
                Italian = request.Content.Italian is null ? null : _sanitizer.Sanitize(request.Content.Italian),
                Russian = request.Content.Russian is null ? null : _sanitizer.Sanitize(request.Content.Russian)
            },
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
