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

        project.Name.Arabic = request.Name.Arabic ?? project.Name.Arabic;
        project.Name.English = request.Name.English;
        project.Name.Italian = request.Name.Italian ?? project.Name.Italian;
        project.Name.Russian = request.Name.Russian ?? project.Name.Russian;

        project.Description.Arabic = request.Description.Arabic ?? project.Description.Arabic;
        project.Description.English = request.Description.English;
        project.Description.Italian = request.Description.Italian ?? project.Description.Italian;
        project.Description.Russian = request.Description.Russian ?? project.Description.Russian;

        project.Location.Arabic = request.Location.Arabic ?? project.Location.Arabic;
        project.Location.English = request.Location.English;
        project.Location.Italian = request.Location.Italian ?? project.Location.Italian;
        project.Location.Russian = request.Location.Russian ?? project.Location.Russian;
        
        project.YearOfConstruction = request.YearOfConstruction;
        project.NumberOfVillas = request.NumberOfVillas;
        project.NumberOfApartments = request.NumberOfApartments;
        project.Area = request.Area;
        project.Status = request.Status;
        project.Types = request.Types;
       
        project.PriceRange = request.PriceRange;

        await _projectRepository.UpdateAsync(project, cancellationToken);
        _cache.Remove("projects-all");
        _cache.Remove($"projects-{project.Id}");
        return Result.Success();
    }
}