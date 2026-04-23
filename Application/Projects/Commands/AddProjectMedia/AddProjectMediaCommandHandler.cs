using Application.Common;
using Application.Common.Behaviors;
using Domain.Entities.Projects;
using Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Caching.Memory;

namespace Application.Projects.Commands.AddProjectMedia;

public class AddProjectMediaCommandHandler : IRequestHandler<AddProjectMediaCommand, Result>
{
    private readonly IProjectRepository _projectRepository;
    private readonly IMediaStorageService _mediaStorage;
    private readonly IMemoryCache  _cache;

    public AddProjectMediaCommandHandler(
        IProjectRepository projectRepository,
        IMediaStorageService mediaStorage,
        IMemoryCache cache)
    {
        _projectRepository = projectRepository;
        _mediaStorage = mediaStorage;
        _cache = cache;
    }

    public async Task<Result> Handle(AddProjectMediaCommand request, CancellationToken cancellationToken)
    {
        var project = await _projectRepository.GetByIdAsync(request.ProjectId, cancellationToken);

        if (project is null)
            return Result.Failure("Project.NotFound", "Project not found", 404);

        foreach (var item in request.Images)
        {
            var url = await _mediaStorage.SaveAsync(
                item.Image,
                $"projects/{project.Id}/{item.Category.ToString().ToLower()}",
                cancellationToken);

            project.Media.Add(new ProjectMedia
            {
                ProjectId = project.Id,
                Url = url,
                Category = item.Category
            });
        }

        await _projectRepository.UpdateAsync(project, cancellationToken);

        _cache.Remove("projects-all");
        _cache.Remove($"projects-{project.Id}");

        return Result.Success();
    }

    
}