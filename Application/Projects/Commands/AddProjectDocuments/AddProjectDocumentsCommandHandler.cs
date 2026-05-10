using Application.Common;
using Application.Common.Interfaces;
using Domain.Entities;
using Domain.Entities.Projects;
using Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Caching.Memory;

namespace Application.Projects.Commands.AddProjectDocuments;

public class AddProjectDocumentsCommandHandler : IRequestHandler<AddProjectDocumentsCommand, Result>
{
    private readonly IProjectRepository _projectRepository;
    private readonly IMediaStorageService _mediaStorage;
    private readonly IMemoryCache _cache;

    public AddProjectDocumentsCommandHandler(
        IProjectRepository projectRepository,
        IMediaStorageService mediaStorage,
        IMemoryCache cache)
    {
        _projectRepository = projectRepository;
        _mediaStorage = mediaStorage;
        _cache = cache;
    }

    public async Task<Result> Handle(
        AddProjectDocumentsCommand request,
        CancellationToken cancellationToken)
    {
        var project = await _projectRepository.GetByIdAsync(request.ProjectId, cancellationToken);

        if (project is null)
            return Result.Failure("Project.NotFound", "Project not found", 404);

        foreach (var item in request.Documents)
        {
            var url = await _mediaStorage.SaveAsync(
                item.Document,
                $"projects/{project.Id}/documents/{item.Category.ToString().ToLower()}",
                cancellationToken);

            project.Documents.Add(new ProjectDocument
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