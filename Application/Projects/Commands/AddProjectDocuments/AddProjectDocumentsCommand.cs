using Application.Common;
using Application.Projects.Dtos;
using MediatR;

namespace Application.Projects.Commands.AddProjectDocuments;

public class AddProjectDocumentsCommand : IRequest<Result>
{
    public int ProjectId { get; set; }
    public List<ProjectDocumentRequestDto> Documents { get; set; } = [];
}