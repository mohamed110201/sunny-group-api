using Application.Common;
using Application.Common.Behaviors;
using Application.Common.Interfaces;
using Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Caching.Memory;

namespace Application.Projects.Commands.DeleteAttraction;

public class DeleteAttractionCommandHandler : IRequestHandler<DeleteAttractionCommand, Result>
{
    private readonly IProjectRepository _projectRepository;
    private readonly IMediaStorageService _mediaStorage;
    private readonly IMemoryCache _cache;

    public DeleteAttractionCommandHandler(
        IProjectRepository projectRepository,
        IMediaStorageService mediaStorage,
        IMemoryCache cache)
    {
        _projectRepository = projectRepository;
        _mediaStorage = mediaStorage;
        _cache = cache;
    }

    public async Task<Result> Handle(DeleteAttractionCommand request, CancellationToken cancellationToken)
    {
        var project = await _projectRepository.GetByIdAsync(request.ProjectId, cancellationToken);

        if (project is null)
            return Result.Failure("Project.NotFound", "Project not found", 404);

        var attraction = project.Attractions.FirstOrDefault(a => a.Id == request.AttractionId);

        if (attraction is null)
            return Result.Failure("Attraction.NotFound", "Attraction not found", 404);

        if (!string.IsNullOrEmpty(attraction.IconUrl))
            await _mediaStorage.DeleteAsync(attraction.IconUrl, cancellationToken);

        project.Attractions.Remove(attraction);
        await _projectRepository.UpdateAsync(project, cancellationToken);

        _cache.Remove("projects-all");
        _cache.Remove($"projects-{project.Id}");

        return Result.Success();
    }
}