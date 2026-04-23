using Application.Common;
using Domain.Interfaces;
using MediatR;

namespace Application.Projects.Commands.DeleteProject;

public class DeleteProjectCommandHandler : IRequestHandler<DeleteProjectCommand, Result>
{
    private readonly IProjectRepository _projectRepository;
    private readonly IMediaStorageService _mediaStorage;

    public DeleteProjectCommandHandler(
        IProjectRepository projectRepository,
        IMediaStorageService mediaStorage)
    {
        _projectRepository = projectRepository;
        _mediaStorage = mediaStorage;
    }

    public async Task<Result> Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
    {
        var project = await _projectRepository.GetByIdAsync(request.Id, cancellationToken);

        if (project is null)
            return Result.Failure("Project.NotFound", "Project not found", 404);

        foreach (var media in project.Media)
            await _mediaStorage.DeleteAsync(media.Url, cancellationToken);

        foreach (var attraction in project.Attractions)
            if (!string.IsNullOrEmpty(attraction.IconUrl))
                await _mediaStorage.DeleteAsync(attraction.IconUrl, cancellationToken);

        await _projectRepository.DeleteAsync(project, cancellationToken);

        return Result.Success();
    }
}