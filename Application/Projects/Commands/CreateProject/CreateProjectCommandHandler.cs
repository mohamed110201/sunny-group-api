using Application.Common;
using Application.Common.Interfaces;
using Domain.Entities.Projects;
using Domain.Enums;
using Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Caching.Memory;

namespace Application.Projects.Commands.CreateProject;

public class CreateProjectCommandHandler : IRequestHandler<CreateProjectCommand, Result<int>>
{
    private readonly IProjectRepository _projectRepository;
    private readonly IMediaStorageService _mediaStorage;
    private readonly IMemoryCache _cache;


    public CreateProjectCommandHandler(
        IProjectRepository projectRepository,
        IMediaStorageService mediaStorage,
        IMemoryCache cache)
    {
        _projectRepository = projectRepository;
        _mediaStorage = mediaStorage;
        _cache = cache;
    }

    public async Task<Result<int>> Handle(
        CreateProjectCommand request,
        CancellationToken cancellationToken)
    {
        var project = new Project
        {
            Name = request.Name,
            Description = request.Description,
            MetaTitle = request.MetaTitle,
            MetaDescription = request.MetaDescription,
            YearOfConstruction = request.YearOfConstruction,
            NumberOfVillas = request.NumberOfVillas,
            NumberOfApartments = request.NumberOfApartments,
            Area = request.Area,
            Location = request.Location,
            Status = request.Status,
            Types = request.Types,
            PriceRange = request.PriceRange,
        };

        await _projectRepository.AddAsync(project, cancellationToken);
        
        await _projectRepository.UpdateAsync(project, cancellationToken);
        _cache.Remove("projects-all");
        _cache.Remove($"projects-{project.Id}");
        return Result<int>.Success(project.Id);
    }
}