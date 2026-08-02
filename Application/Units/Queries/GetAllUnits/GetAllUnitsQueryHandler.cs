using Application.Common;
using Application.Units.Dtos;
using Application.Units.Mapping;
using Domain.Interfaces;
using MediatR;

namespace Application.Units.Queries.GetAllUnits;

public class GetAllUnitsQueryHandler : IRequestHandler<GetAllUnitsQuery, Result<List<ProjectUnitsGroupDto>>>
{
    private readonly IUnitRepository _unitRepository;

    public GetAllUnitsQueryHandler(IUnitRepository unitRepository)
    {
        _unitRepository = unitRepository;
    }

    public async Task<Result<List<ProjectUnitsGroupDto>>> Handle(
        GetAllUnitsQuery request,
        CancellationToken cancellationToken)
    {
        var units = await _unitRepository.GetAllAsync(cancellationToken);

        var grouped = units
            .GroupBy(u => u.Project)
            .Select(g => new ProjectUnitsGroupDto
            {
                ProjectId = g.Key.Id,
                ProjectName = g.Key.Name,
                ProjectLocation = g.Key.Location,
                MapUrl = g.Key.MapUrl,
                Units = g.Select(u => u.ToGetAllDto()).ToList()
            })
            .ToList();

        return Result<List<ProjectUnitsGroupDto>>.Success(grouped);
    }
}