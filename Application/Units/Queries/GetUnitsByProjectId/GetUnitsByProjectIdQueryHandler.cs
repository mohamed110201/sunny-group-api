using Application.Common;
using Application.Units.Dtos;
using Application.Units.Mapping;
using Domain.Interfaces;
using MediatR;

namespace Application.Units.Queries.GetUnitsByProjectId;

public class GetUnitsByProjectIdQueryHandler : IRequestHandler<GetUnitsByProjectIdQuery, Result<List<GetAllUnitsDto>>>
{
    private readonly IUnitRepository _unitRepository;
    private readonly IProjectRepository _projectRepository;

    public GetUnitsByProjectIdQueryHandler(
        IUnitRepository unitRepository,
        IProjectRepository projectRepository)
    {
        _unitRepository = unitRepository;
        _projectRepository = projectRepository;
    }

    public async Task<Result<List<GetAllUnitsDto>>> Handle(
        GetUnitsByProjectIdQuery request,
        CancellationToken cancellationToken)
    {
        var projectExists = await _projectRepository.ExistsAsync(request.ProjectId, cancellationToken);

        if (!projectExists)
            return Result<List<GetAllUnitsDto>>.Failure("Project.NotFound", "Project not found", 404);

        var units = await _unitRepository.GetByProjectIdAsync(request.ProjectId, cancellationToken);

        return Result<List<GetAllUnitsDto>>.Success(units.Select(u => u.ToGetAllDto()).ToList());
    }
}