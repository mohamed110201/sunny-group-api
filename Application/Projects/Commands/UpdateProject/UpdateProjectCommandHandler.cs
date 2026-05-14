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

        if (request.Name != null)
        {
            project.Name.Arabic = request.Name.Arabic ?? project.Name.Arabic;
            project.Name.English = request.Name.English ?? project.Name.English;
            project.Name.Italian = request.Name.Italian ?? project.Name.Italian;
            project.Name.Russian = request.Name.Russian ?? project.Name.Russian;
        }

        if (request.Description != null)
        {
            project.Description.Arabic = request.Description.Arabic ?? project.Description.Arabic;
            project.Description.English = request.Description.English ?? project.Description.English;
            project.Description.Italian = request.Description.Italian ?? project.Description.Italian;
            project.Description.Russian = request.Description.Russian ?? project.Description.Russian;
        }

        if (request.MetaTitle != null)
        {
            project.MetaTitle.Arabic = request.MetaTitle.Arabic ?? project.MetaTitle.Arabic;
            project.MetaTitle.English = request.MetaTitle.English ?? project.MetaTitle.English;
            project.MetaTitle.Italian = request.MetaTitle.Italian ?? project.MetaTitle.Italian;
            project.MetaTitle.Russian = request.MetaTitle.Russian ?? project.MetaTitle.Russian;
        }

        if (request.MetaDescription != null)
        {
            project.MetaDescription.Arabic = request.MetaDescription.Arabic ?? project.MetaDescription.Arabic;
            project.MetaDescription.English = request.MetaDescription.English ?? project.MetaDescription.English;
            project.MetaDescription.Italian = request.MetaDescription.Italian ?? project.MetaDescription.Italian;
            project.MetaDescription.Russian = request.MetaDescription.Russian ?? project.MetaDescription.Russian;
        }

        if (request.Location != null)
        {
            project.Location.Arabic = request.Location.Arabic ?? project.Location.Arabic;
            project.Location.English = request.Location.English ?? project.Location.English;
            project.Location.Italian = request.Location.Italian ?? project.Location.Italian;
            project.Location.Russian = request.Location.Russian ?? project.Location.Russian;
        }

        if (request.YearOfConstruction.HasValue)
            project.YearOfConstruction = request.YearOfConstruction.Value;

        if (request.NumberOfVillas.HasValue)
            project.NumberOfVillas = request.NumberOfVillas.Value;

        if (request.NumberOfApartments.HasValue)
            project.NumberOfApartments = request.NumberOfApartments.Value;

        if (request.Area.HasValue)
            project.Area = request.Area.Value;

        if (request.Status.HasValue)
            project.Status = request.Status.Value;

        if (request.Types != null)
            project.Types = request.Types;

        await _projectRepository.UpdateAsync(project, cancellationToken);
        _cache.Remove("projects-all");
        _cache.Remove($"projects-{project.Id}");
        return Result.Success();
    }
}