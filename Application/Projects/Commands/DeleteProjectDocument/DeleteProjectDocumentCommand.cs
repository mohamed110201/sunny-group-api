using Application.Common;
using MediatR;

namespace Application.Projects.Commands.DeleteProjectDocument;

public record DeleteProjectDocumentCommand(int ProjectId, int DocumentId) : IRequest<Result>;