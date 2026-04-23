using Application.Common;
using Application.Projects.Dtos;
using Application.Projects.Mapping;
using Domain.Interfaces;
using MediatR;

namespace Application.Projects.Queries.GetProjectById;

public class GetProjectByIdQueryHandler
{
    public class GetProjectByIdHandler : IRequestHandler<GetProjectByIdQuery, Result<GetProjectByIdDto>>
    {
        private readonly IProjectRepository _projectRepository;

        public GetProjectByIdHandler(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<Result<GetProjectByIdDto>> Handle(
            GetProjectByIdQuery request,
            CancellationToken cancellationToken)
        {
            var project = await _projectRepository.GetByIdAsync(request.Id, cancellationToken);

            if (project is null)
                return Result<GetProjectByIdDto>.Failure("Project.NotFound", "Project not found", 404);

            return Result<GetProjectByIdDto>.Success(project.ToGetByIdDto());
        }
    }
}