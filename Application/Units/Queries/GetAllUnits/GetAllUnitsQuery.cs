using Application.Common;
using Application.Common.Behaviors;
using Application.Units.Dtos;
using MediatR;

namespace Application.Units.Queries.GetAllUnits;

public record GetAllUnitsQuery : IRequest<Result<List<ProjectUnitsGroupDto>>>, ICacheable
{
    public string CacheKey => "units-all";
    public TimeSpan? CacheDuration => TimeSpan.FromMinutes(30);
}