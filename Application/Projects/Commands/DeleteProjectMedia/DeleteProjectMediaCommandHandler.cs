using Application.Common;
using Application.Common.Behaviors;
using Application.Common.Interfaces;
using Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Caching.Memory;

namespace Application.Projects.Commands.DeleteProjectMedia;


    public class DeleteProjectMediaCommandHandler : IRequestHandler<DeleteProjectMediaCommand, Result>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IMediaStorageService _mediaStorage;
        private readonly IMemoryCache _cache;

        public DeleteProjectMediaCommandHandler(
            IProjectRepository projectRepository,
            IMediaStorageService mediaStorage,
            IMemoryCache cache)
        {
            _projectRepository = projectRepository;
            _mediaStorage = mediaStorage;
            _cache = cache;
        }

        public async Task<Result> Handle(DeleteProjectMediaCommand request, CancellationToken cancellationToken)
        {
            var project = await _projectRepository.GetByIdAsync(request.ProjectId, cancellationToken);

            if (project is null)
                return Result.Failure("Project.NotFound", "Project not found", 404);

            var media = project.Media.FirstOrDefault(m => m.Id == request.MediaId);

            if (media is null)
                return Result.Failure("Media.NotFound", "Media not found", 404);

            await _mediaStorage.DeleteAsync(media.Url, cancellationToken);
            project.Media.Remove(media);
            await _projectRepository.UpdateAsync(project, cancellationToken);

            _cache.Remove("projects-all");
            _cache.Remove($"projects-{project.Id}");

            return Result.Success();
        }
    }
