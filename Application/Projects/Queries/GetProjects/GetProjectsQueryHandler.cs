using Application.Common;
using Application.Projects.Dtos;
using Application.Projects.Mapping;
using Domain.Interfaces;
using MediatR;

namespace Application.Projects.Queries.GetProjects;

public class GetAllProjectsHandler : IRequestHandler<GetAllProjectsQuery, Result<List<GetAllProjectsDto>>>
{
    private readonly IProjectRepository _projectRepository;

    public GetAllProjectsHandler(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public async Task<Result<List<GetAllProjectsDto>>> Handle(
        GetAllProjectsQuery request,
        CancellationToken cancellationToken)
    {
        var projects = await _projectRepository.GetAllAsync(cancellationToken);

        var dtos = projects.Select(p => p.ToGetAllDto()).ToList();

        return Result<List<GetAllProjectsDto>>.Success(dtos);
    }
}