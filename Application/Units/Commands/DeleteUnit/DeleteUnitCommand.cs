using Application.Common;
using MediatR;

namespace Application.Units.Commands.DeleteUnit;

public record DeleteUnitCommand(int ProjectId, int UnitId) : IRequest<Result>;