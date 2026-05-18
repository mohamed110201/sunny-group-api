using Application.Common;
using Application.Common.Interfaces;
using MediatR;

namespace Application.Blogs.Commands.UploadBlogContentImage;

public class UploadBlogContentImageCommandHandler : IRequestHandler<UploadBlogContentImageCommand, Result<string>>
{
    private readonly IMediaStorageService _mediaStorage;

    public UploadBlogContentImageCommandHandler(IMediaStorageService mediaStorage)
    {
        _mediaStorage = mediaStorage;
    }

    public async Task<Result<string>> Handle(
        UploadBlogContentImageCommand request,
        CancellationToken cancellationToken)
    {
        var url = await _mediaStorage.SaveAsync(
            request.File,
            "blogs/content-images",
            cancellationToken);

        return Result<string>.Success(url);
    }
}
