using Application.Common;
using Application.Common.Behaviors;
using Application.Common.Interfaces;
using Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Caching.Memory;

namespace Application.Projects.Commands.UpdateAttraction;

public class UpdateAttractionCommandHandler : IRequestHandler<UpdateAttractionCommand, Result>
{
    private readonly IProjectRepository _projectRepository;
    private readonly IMediaStorageService _mediaStorage;
    private readonly IMemoryCache _cache;

    public UpdateAttractionCommandHandler(
        IProjectRepository projectRepository,
        IMediaStorageService mediaStorage,
        IMemoryCache cache)
    {
        _projectRepository = projectRepository;
        _mediaStorage = mediaStorage;
        _cache = cache;
    }

    public async Task<Result> Handle(UpdateAttractionCommand request, CancellationToken cancellationToken)
    {
        var project = await _projectRepository.GetByIdAsync(request.ProjectId, cancellationToken);

        if (project is null)
            return Result.Failure("Project.NotFound", "Project not found", 404);

        var attraction = project.Attractions.FirstOrDefault(a => a.Id == request.Id);

        if (attraction is null)
            return Result.Failure("Attraction.NotFound", "Attraction not found", 404);

        if (request.Name != null)
        {
            attraction.Name.English = request.Name.English ?? attraction.Name.English;
            attraction.Name.Arabic = request.Name.Arabic ?? attraction.Name.Arabic;
            attraction.Name.Italian = request.Name.Italian ?? attraction.Name.Italian;
            attraction.Name.Russian = request.Name.Russian ?? attraction.Name.Russian;
        }

        if (request.TimeInMinutes.HasValue)
            attraction.TimeInMinutes = request.TimeInMinutes.Value;

        if (request.RemoveIcon)
        {
            if (!string.IsNullOrEmpty(attraction.IconUrl))
                await _mediaStorage.DeleteAsync(attraction.IconUrl, cancellationToken);
            attraction.IconUrl = string.Empty;
        }
        else if (request.Icon != null)
        {
            if (!string.IsNullOrEmpty(attraction.IconUrl))
                await _mediaStorage.DeleteAsync(attraction.IconUrl, cancellationToken);

            attraction.IconUrl = await _mediaStorage.SaveAsync(
                request.Icon,
                $"projects/{request.ProjectId}/attractions",
                cancellationToken);
        }

        await _projectRepository.UpdateAsync(project, cancellationToken);

        _cache.Remove("projects-all");
        _cache.Remove($"projects-{project.Id}");

        return Result.Success();
    }
}