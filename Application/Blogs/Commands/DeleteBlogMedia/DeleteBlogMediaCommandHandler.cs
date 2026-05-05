using Application.Common;
using Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Caching.Memory;

namespace Application.Blogs.Commands.DeleteBlogMedia;

public class DeleteBlogMediaCommandHandler : IRequestHandler<DeleteBlogMediaCommand, Result>
{
    private readonly IBlogRepository _blogRepository;
    private readonly IMediaStorageService _mediaStorage;
    private readonly IMemoryCache _cache;

    public DeleteBlogMediaCommandHandler(
        IBlogRepository blogRepository,
        IMediaStorageService mediaStorage,
        IMemoryCache cache)
    {
        _blogRepository = blogRepository;
        _mediaStorage = mediaStorage;
        _cache = cache;
    }

    public async Task<Result> Handle(DeleteBlogMediaCommand request, CancellationToken cancellationToken)
    {
        var blog = await _blogRepository.GetByIdAsync(request.BlogId, cancellationToken);

        if (blog is null)
            return Result.Failure("Blog.NotFound", "Blog not found", 404);

        var media = blog.Media.FirstOrDefault(m => m.Id == request.MediaId);

        if (media is null)
            return Result.Failure("Media.NotFound", "Media not found", 404);

        await _mediaStorage.DeleteAsync(media.Url, cancellationToken);
        blog.Media.Remove(media);
        await _blogRepository.UpdateAsync(blog, cancellationToken);

        _cache.Remove("blogs-all");
        _cache.Remove($"blogs-{blog.Id}");

        return Result.Success();
    }
}