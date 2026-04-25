using Application.Common;
using Domain.Enums;
using Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Caching.Memory;

namespace Application.Projects.Commands.UpdateProject;

public class UpdateProjectCommandHandler : IRequestHandler<UpdateProjectCommand, Result>
{
    private readonly IProjectRepository _projectRepository;
    private readonly IMemoryCache _cache;


    public UpdateProjectCommandHandler(IProjectRepository projectRepository
    , IMemoryCache cache)
    {
        _projectRepository = projectRepository;
        _cache = cache;
    }

    public async Task<Result> Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
    {
        var project = await _projectRepository.GetByIdAsync(request.Id, cancellationToken);

        if (project is null)
            return Result.Failure("Project.NotFound", "Project not found", 404);

        project.Name = request.Name;
        project.Description = request.Description;
        project.YearOfConstruction = request.YearOfConstruction;
        project.NumberOfVillas = request.NumberOfVillas;
        project.NumberOfApartments = request.NumberOfApartments;
        project.Area = request.Area;
        project.Location = request.Location;
        project.Status = request.Status;
        project.Types = request.Types;
       
        project.PriceRange = request.PriceRange;

        await _projectRepository.UpdateAsync(project, cancellationToken);
        _cache.Remove("projects-all");
        _cache.Remove($"projects-{project.Id}");
        return Result.Success();
    }
}