using Application.Common;
using Application.Common.Behaviors;
using Application.Projects.Dtos;
using MediatR;

namespace Application.Projects.Queries.GetProjectById;

public record GetProjectByIdQuery(int Id) : IRequest<Result<GetProjectByIdDto>>, ICacheable
{
    public string CacheKey => $"projects-{Id}";
    public TimeSpan? CacheDuration => TimeSpan.FromMinutes(10);
}