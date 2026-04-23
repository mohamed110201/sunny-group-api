using Application.Common;
using MediatR;

namespace Application.Projects.Commands.DeleteAttraction;

public record DeleteAttractionCommand(int ProjectId, int AttractionId) : IRequest<Result>;
