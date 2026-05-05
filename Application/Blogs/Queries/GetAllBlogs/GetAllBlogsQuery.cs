using Application.Blogs.Dtos;
using Application.Common;
using Application.Common.Behaviors;
using MediatR;

namespace Application.Blogs.Queries.GetAllBlogs;

public record GetAllBlogsQuery : IRequest<Result<List<GetAllBlogsDto>>>, ICacheable
{
    public string CacheKey => "blogs-all";
    public TimeSpan? CacheDuration => TimeSpan.FromMinutes(30);
}