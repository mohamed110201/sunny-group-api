using Application.Common;
using MediatR;

namespace Application.Projects.Commands.DeleteProject;

public record DeleteProjectCommand(int Id) : IRequest<Result>;