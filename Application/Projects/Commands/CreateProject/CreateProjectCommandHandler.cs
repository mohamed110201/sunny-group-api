using Application.Common;
using Domain.Entities.Projects;
using Domain.Enums;
using Domain.Interfaces;
using MediatR;

namespace Application.Projects.Commands.CreateProject;

public class CreateProjectCommandHandler : IRequestHandler<CreateProjectCommand, Result<int>>
{
    private readonly IProjectRepository _projectRepository;
    private readonly IMediaStorageService _mediaStorage;

    public CreateProjectCommandHandler(
        IProjectRepository projectRepository,
        IMediaStorageService mediaStorage)
    {
        _projectRepository = projectRepository;
        _mediaStorage = mediaStorage;
    }

    public async Task<Result<int>> Handle(
        CreateProjectCommand request,
        CancellationToken cancellationToken)
    {
        var project = new Project
        {
            Name = request.Name,
            Description = request.Description,
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

        return Result<int>.Success(project.Id);
    }
}