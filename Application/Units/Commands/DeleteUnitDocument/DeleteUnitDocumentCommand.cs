using Application.Common;
using MediatR;

namespace Application.Units.Commands.DeleteUnitDocument;

public record DeleteUnitDocumentCommand(int ProjectId, int UnitId, int DocumentId) : IRequest<Result>;