using Application.Common;
using Application.Common.Interfaces;
using Domain.Entities;
using Domain.Entities.Blogs;
using Domain.Enums;
using Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Caching.Memory;

namespace Application.Blogs.Commands.AddBlogMedia;

public class AddBlogMediaCommandHandler : IRequestHandler<AddBlogMediaCommand, Result>
{
    private readonly IBlogRepository _blogRepository;
    private readonly IMediaStorageService _mediaStorage;
    private readonly IMemoryCache _cache;

    public AddBlogMediaCommandHandler(
        IBlogRepository blogRepository,
        IMediaStorageService mediaStorage,
        IMemoryCache cache)
    {
        _blogRepository = blogRepository;
        _mediaStorage = mediaStorage;
        _cache = cache;
    }

    public async Task<Result> Handle(AddBlogMediaCommand request, CancellationToken cancellationToken)
    {
        var blog = await _blogRepository.GetByIdAsync(request.BlogId, cancellationToken);

        if (blog is null)
            return Result.Failure("Blog.NotFound", "Blog not found", 404);

        var incomingCovers = request.Images.Count(i => i.Category == MediaCategory.Cover);

        if (incomingCovers > 1)
            return Result.Failure("Media.InvalidCover", "Only one cover image is allowed", 400);

        if (incomingCovers == 1 && blog.Media.Any(m => m.Category == MediaCategory.Cover))
            return Result.Failure("Media.CoverExists", "Blog already has a cover image. Delete it first", 400);

        foreach (var item in request.Images)
        {
            var url = await _mediaStorage.SaveAsync(
                item.Image,
                $"blogs/{blog.Id}/{item.Category.ToString().ToLower()}",
                cancellationToken);

            blog.Media.Add(new BlogMedia
            {
                BlogId = blog.Id,
                Url = url,
                Category = item.Category
            });
        }

        await _blogRepository.UpdateAsync(blog, cancellationToken);

        _cache.Remove("blogs-all");
        _cache.Remove($"blogs-{blog.Id}");

        return Result.Success();
    }
}