using Application.Common;
using Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Caching.Memory;

namespace Application.Blogs.Commands.UpdateBlog;

public class UpdateBlogCommandHandler : IRequestHandler<UpdateBlogCommand, Result>
{
    private readonly IBlogRepository _blogRepository;
    private readonly IMemoryCache _cache;

    public UpdateBlogCommandHandler(
        IBlogRepository blogRepository,
        IMemoryCache cache)
    {
        _blogRepository = blogRepository;
        _cache = cache;
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
            blog.Content.English = request.Content.English ?? blog.Content.English;
            blog.Content.Arabic = request.Content.Arabic ?? blog.Content.Arabic;
            blog.Content.Italian = request.Content.Italian ?? blog.Content.Italian;
            blog.Content.Russian = request.Content.Russian ?? blog.Content.Russian;
        }

        if (request.MetaTitle != null)
            blog.MetaTitle = request.MetaTitle;

        if (request.MetaDescription != null)
            blog.MetaDescription = request.MetaDescription;

        if (request.Category.HasValue)
            blog.Category = request.Category.Value;

        await _blogRepository.UpdateAsync(blog, cancellationToken);

        _cache.Remove("blogs-all");
        _cache.Remove($"blogs-{blog.Id}");

        return Result.Success();
    }
}