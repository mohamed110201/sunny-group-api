using Application.Common;
using Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Caching.Memory;

namespace Application.Blogs.Commands.UpdateBlogCategory;

public class UpdateBlogCategoryCommandHandler : IRequestHandler<UpdateBlogCategoryCommand, Result>
{
    private readonly IBlogCategoryRepository _categoryRepository;
    private readonly IMemoryCache _cache;

    public UpdateBlogCategoryCommandHandler(
        IBlogCategoryRepository categoryRepository,
        IMemoryCache cache)
    {
        _categoryRepository = categoryRepository;
        _cache = cache;
    }

    public async Task<Result> Handle(
        UpdateBlogCategoryCommand request,
        CancellationToken cancellationToken)
    {
        var category = await _categoryRepository.GetByIdAsync(request.Id, cancellationToken);
        if (category == null)
            return Result.Failure("Category.NotFound","Category not found or is inactive",404);

        if (request.Name != null)
            category.Name = request.Name;

        await _categoryRepository.UpdateAsync(category, cancellationToken);

        _cache.Remove("blog-categories");

        return Result.Success();
    }
}
