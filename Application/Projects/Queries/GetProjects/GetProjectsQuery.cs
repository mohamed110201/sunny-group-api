using Application.Common;
using Application.Common.Behaviors;
using Application.Projects.Dtos;
using MediatR;

namespace Application.Projects.Queries.GetProjects;

public record GetAllProjectsQuery : IRequest<Result<List<GetAllProjectsDto>>>, ICacheable
{
    public string CacheKey => "projects-all";
    public TimeSpan? CacheDuration => TimeSpan.FromMinutes(10);
}