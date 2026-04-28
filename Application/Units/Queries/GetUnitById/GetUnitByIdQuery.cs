using Application.Common;
using Application.Common.Behaviors;
using Application.Units.Dtos;
using MediatR;

namespace Application.Units.Queries.GetUnitById;

public record GetUnitByIdQuery(int ProjectId, int UnitId) : IRequest<Result<GetUnitByIdDto>>, ICacheable
{
    public string CacheKey => $"units-{UnitId}";
    public TimeSpan? CacheDuration => TimeSpan.FromMinutes(30);
}