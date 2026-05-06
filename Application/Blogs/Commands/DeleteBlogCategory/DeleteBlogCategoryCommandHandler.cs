using Application.Common;
using Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Caching.Memory;

namespace Application.Blogs.Commands.DeleteBlogCategory;

public class DeleteBlogCategoryCommandHandler : IRequestHandler<DeleteBlogCategoryCommand, Result>
{
    private readonly IBlogCategoryRepository _categoryRepository;
    private readonly IMemoryCache _cache;

    public DeleteBlogCategoryCommandHandler(
        IBlogCategoryRepository categoryRepository,
        IMemoryCache cache)
    {
        _categoryRepository = categoryRepository;
        _cache = cache;
    }

    public async Task<Result> Handle(
        DeleteBlogCategoryCommand request,
        CancellationToken cancellationToken)
    {
        var category = await _categoryRepository.GetByIdAsync(request.Id, cancellationToken);
        if (category == null)
            return Result.Failure("Category.NotFound","Category not found or is inactive",404);


        var blogCount = await _categoryRepository.GetCategoryBlogCountAsync(request.Id, cancellationToken);
        if (blogCount > 0)
            return Result.Failure("Category.Conflict",$"Cannot delete category with {blogCount} blog(s). Please reassign the blogs to another category first.");

        category.IsActive = false;
        await _categoryRepository.UpdateAsync(category, cancellationToken);

        _cache.Remove("blog-categories");

        return Result.Success();
    }
}
