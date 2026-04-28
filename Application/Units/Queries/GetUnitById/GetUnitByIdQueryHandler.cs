using Application.Common;
using Application.Units.Dtos;
using Application.Units.Mapping;
using Domain.Interfaces;
using MediatR;

namespace Application.Units.Queries.GetUnitById;

public class GetUnitByIdQueryHandler : IRequestHandler<GetUnitByIdQuery, Result<GetUnitByIdDto>>
{
    private readonly IUnitRepository _unitRepository;

    public GetUnitByIdQueryHandler(IUnitRepository unitRepository)
    {
        _unitRepository = unitRepository;
    }

    public async Task<Result<GetUnitByIdDto>> Handle(
        GetUnitByIdQuery request,
        CancellationToken cancellationToken)
    {
        var unit = await _unitRepository.GetByIdAsync(request.UnitId, cancellationToken);

        if (unit is null || unit.ProjectId != request.ProjectId)
            return Result<GetUnitByIdDto>.Failure("Unit.NotFound", "Unit not found", 404);

        return Result<GetUnitByIdDto>.Success(unit.ToGetByIdDto());
    }
}