using Application.Common;
using MediatR;

namespace Application.Units.Commands.DeleteUnitMedia;

public record DeleteUnitMediaCommand(int ProjectId, int UnitId, int MediaId) : IRequest<Result>;