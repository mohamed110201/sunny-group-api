using Application.Common;
using Application.Units.Dtos;
using MediatR;

namespace Application.Units.Commands.AddUnitDocuments;

public class AddUnitDocumentsCommand : IRequest<Result>
{
    public int UnitId { get; set; }
    public int ProjectId { get; set; }
    public List<UnitDocumentRequestDto> Documents { get; set; } = [];
}