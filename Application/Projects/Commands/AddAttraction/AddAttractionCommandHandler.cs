using Application.Common;
using Application.Common.Behaviors;
using Domain.Entities.Projects;
using Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Caching.Memory;

namespace Application.Projects.Commands.AddAttraction;

public class AddAttractionCommandHandler : IRequestHandler<AddAttractionCommand, Result>
{
    private readonly IProjectRepository _projectRepository;
    private readonly IMediaStorageService _mediaStorage;
    private readonly IMemoryCache _cache;

    public AddAttractionCommandHandler(
        IProjectRepository projectRepository,
        IMediaStorageService mediaStorage,
        IMemoryCache cache)
    {
        _projectRepository = projectRepository;
        _mediaStorage = mediaStorage;
        _cache = cache;
    }

    public async Task<Result> Handle(AddAttractionCommand request, CancellationToken cancellationToken)
    {
        var project = await _projectRepository.GetByIdAsync(request.ProjectId, cancellationToken);

        if (project is null)
            return Result.Failure("Project.NotFound", "Project not found", 404);

        foreach (var item in request.Attractions)
        {
            var iconUrl = await _mediaStorage.SaveAsync(
                item.Icon,
                $"projects/{project.Id}/attractions",
                cancellationToken);

            project.Attractions.Add(new Attraction
            {
                ProjectId = project.Id,
                Name = item.Name,
                TimeInMinutes = item.TimeInMinutes,
                IconUrl = iconUrl
            });
        }

        await _projectRepository.UpdateAsync(project, cancellationToken);

        _cache.Remove("projects-all");
        _cache.Remove($"projects-{project.Id}");

        return Result.Success();
    }
}