using Application.Common;
using Application.Common.Behaviors;
using Application.Units.Dtos;
using MediatR;

namespace Application.Units.Queries.GetUnitsByProjectId;

public record GetUnitsByProjectIdQuery(int ProjectId) : IRequest<Result<List<GetAllUnitsDto>>>, ICacheable
{
    public string CacheKey => $"units-project-{ProjectId}";
    public TimeSpan? CacheDuration => TimeSpan.FromMinutes(30);
}