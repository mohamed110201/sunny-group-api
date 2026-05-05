using Application.Blogs.Dtos;
using Application.Common;
using Application.Common.Behaviors;
using MediatR;

namespace Application.Blogs.Queries.GetBlogById;

public record GetBlogByIdQuery(int Id) : IRequest<Result<GetBlogByIdDto>>, ICacheable
{
    public string CacheKey => $"blogs-{Id}";
    public TimeSpan? CacheDuration => TimeSpan.FromMinutes(30);
}