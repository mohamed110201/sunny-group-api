using Application.Common;
using Application.Common.Behaviors;
using Application.Home.Dtos;
using MediatR;

namespace Application.Home.Queries.GetHome;

public record GetHomeQuery : IRequest<Result<HomeDto>>, ICacheable
{
    public string CacheKey => "home";
    public TimeSpan? CacheDuration => TimeSpan.FromMinutes(30);
}