using Application.Common;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Blogs.Commands.UploadBlogContentImage;

public class UploadBlogContentImageCommand : IRequest<Result<string>>
{
    public IFormFile File { get; set; } = null!;

    public UploadBlogContentImageCommand(IFormFile file)
    {
        File = file;
    }
}
