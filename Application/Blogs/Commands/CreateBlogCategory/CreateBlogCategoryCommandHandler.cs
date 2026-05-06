using Application.Common;
using Domain.Entities.Blogs;
using Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Caching.Memory;

namespace Application.Blogs.Commands.CreateBlogCategory;

public class CreateBlogCategoryCommandHandler : IRequestHandler<CreateBlogCategoryCommand, Result<int>>
{
    private readonly IBlogCategoryRepository _categoryRepository;
    private readonly IMemoryCache _cache;

    public CreateBlogCategoryCommandHandler(
        IBlogCategoryRepository categoryRepository,
        IMemoryCache cache)
    {
        _categoryRepository = categoryRepository;
        _cache = cache;
    }

    public async Task<Result<int>> Handle(
        CreateBlogCategoryCommand request,
        CancellationToken cancellationToken)
    {
        var category = new BlogCategory
        {
            Name = request.Name,
            IsActive = true
        };

        await _categoryRepository.AddAsync(category, cancellationToken);

        _cache.Remove("blog-categories");

        return Result<int>.Success(category.Id);
    }
}
