using Application.Blogs.Dtos;
using Application.Blogs.Mapping;
using Application.Common;
using Domain.Interfaces;
using MediatR;

namespace Application.Blogs.Queries.GetBlogById;

public class GetBlogByIdQueryHandler : IRequestHandler<GetBlogByIdQuery, Result<GetBlogByIdDto>>
{
    private readonly IBlogRepository _blogRepository;

    public GetBlogByIdQueryHandler(IBlogRepository blogRepository)
    {
        _blogRepository = blogRepository;
    }

    public async Task<Result<GetBlogByIdDto>> Handle(
        GetBlogByIdQuery request,
        CancellationToken cancellationToken)
    {
        var blog = await _blogRepository.GetByIdAsync(request.Id, cancellationToken);

        if (blog is null)
            return Result<GetBlogByIdDto>.Failure("Blog.NotFound", "Blog not found", 404);

        return Result<GetBlogByIdDto>.Success(blog.ToGetByIdDto());
    }
}