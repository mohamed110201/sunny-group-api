using Application.Common;
using Application.Common.Interfaces;
using Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Caching.Memory;

namespace Application.Blogs.Commands.UpdateBlog;

public class UpdateBlogCommandHandler : IRequestHandler<UpdateBlogCommand, Result>
{
    private readonly IBlogRepository _blogRepository;
    private readonly IBlogCategoryRepository _categoryRepository;
    private readonly IMemoryCache _cache;
    private readonly IHtmlSanitizerService _sanitizer;


    public UpdateBlogCommandHandler(
        IBlogRepository blogRepository,
        IBlogCategoryRepository categoryRepository,
        IMemoryCache cache,
        IHtmlSanitizerService sanitizer
        )
    {
        _blogRepository = blogRepository;
        _categoryRepository = categoryRepository;
        _cache = cache;
        _sanitizer  = sanitizer;
    }

    public async Task<Result> Handle(
        UpdateBlogCommand request,
        CancellationToken cancellationToken)
    {
        var blog = await _blogRepository.GetByIdAsync(request.Id, cancellationToken);

        if (blog is null)
            return Result.Failure("Blog.NotFound", "Blog not found", 404);

        if (request.Title != null)
        {
            blog.Title.English = request.Title.English ?? blog.Title.English;
            blog.Title.Arabic = request.Title.Arabic ?? blog.Title.Arabic;
            blog.Title.Italian = request.Title.Italian ?? blog.Title.Italian;
            blog.Title.Russian = request.Title.Russian ?? blog.Title.Russian;
        }

        if (request.Description != null)
        {
            blog.Description.English = request.Description.English ?? blog.Description.English;
            blog.Description.Arabic = request.Description.Arabic ?? blog.Description.Arabic;
            blog.Description.Italian = request.Description.Italian ?? blog.Description.Italian;
            blog.Description.Russian = request.Description.Russian ?? blog.Description.Russian;
        }

        if (request.Content != null)
        {
            blog.Content.English = _sanitizer.Sanitize(request.Content.English);
            blog.Content.Arabic = request.Content.Arabic is null
                ? blog.Content.Arabic
                : _sanitizer.Sanitize(request.Content.Arabic);
            blog.Content.Italian = request.Content.Italian is null
                ? blog.Content.Italian
                : _sanitizer.Sanitize(request.Content.Italian);
            blog.Content.Russian = request.Content.Russian is null
                ? blog.Content.Russian
                : _sanitizer.Sanitize(request.Content.Russian);
        }

        if (request.MetaTitle != null)
            blog.MetaTitle = request.MetaTitle;

        if (request.MetaDescription != null)
            blog.MetaDescription = request.MetaDescription;

        if (request.CategoryId.HasValue)
        {
            var category = await _categoryRepository.GetByIdAsync(request.CategoryId.Value, cancellationToken);
            if (category == null || !category.IsActive)
                return Result.Failure("Category.NotFound","Category not found or is inactive",404);

            blog.CategoryId = request.CategoryId.Value;
        }

        await _blogRepository.UpdateAsync(blog, cancellationToken);

        _cache.Remove("blogs-all");
        _cache.Remove($"blogs-{blog.Id}");

        return Result.Success();
    }
}