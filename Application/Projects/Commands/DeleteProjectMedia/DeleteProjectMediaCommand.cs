using Application.Common;
using MediatR;

namespace Application.Projects.Commands.DeleteProjectMedia;

public class DeleteProjectMediaCommandHandler
{
    public record DeleteProjectMediaCommand(int ProjectId, int MediaId) : IRequest<Result>;

}