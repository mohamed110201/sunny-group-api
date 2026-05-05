using Application.Blogs.Dtos;
using Application.Blogs.Mapping;
using Application.Common;
using Domain.Interfaces;
using MediatR;

namespace Application.Blogs.Queries.GetAllBlogs;

public class GetAllBlogsQueryHandler : IRequestHandler<GetAllBlogsQuery, Result<List<GetAllBlogsDto>>>
{
    private readonly IBlogRepository _blogRepository;

    public GetAllBlogsQueryHandler(IBlogRepository blogRepository)
    {
        _blogRepository = blogRepository;
    }

    public async Task<Result<List<GetAllBlogsDto>>> Handle(
        GetAllBlogsQuery request,
        CancellationToken cancellationToken)
    {
        var blogs = await _blogRepository.GetAllAsync(cancellationToken);
        return Result<List<GetAllBlogsDto>>.Success(
            blogs.Select(b => b.ToGetAllDto()).ToList());
    }
}