using Application.Common;
using Application.Common.Interfaces;
using Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Caching.Memory;

namespace Application.Blogs.Commands.DeleteBlog;

public class DeleteBlogCommandHandler : IRequestHandler<DeleteBlogCommand, Result>
{
    private readonly IBlogRepository _blogRepository;
    private readonly IMediaStorageService _mediaStorage;
    private readonly IMemoryCache _cache;

    public DeleteBlogCommandHandler(
        IBlogRepository blogRepository,
        IMediaStorageService mediaStorage,
        IMemoryCache cache)
    {
        _blogRepository = blogRepository;
        _mediaStorage = mediaStorage;
        _cache = cache;
    }

    public async Task<Result> Handle(
        DeleteBlogCommand request,
        CancellationToken cancellationToken)
    {
        var blog = await _blogRepository.GetByIdAsync(request.Id, cancellationToken);

        if (blog is null)
            return Result.Failure("Blog.NotFound", "Blog not found", 404);

        foreach (var media in blog.Media)
            await _mediaStorage.DeleteAsync(media.Url, cancellationToken);

        await _blogRepository.DeleteAsync(blog, cancellationToken);

        _cache.Remove("blogs-all");
        _cache.Remove($"blogs-{blog.Id}");

        return Result.Success();
    }
}