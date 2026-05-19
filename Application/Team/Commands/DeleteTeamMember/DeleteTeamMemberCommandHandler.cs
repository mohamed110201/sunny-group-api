using Application.Common;
using Application.Common.Interfaces;
using Domain.Interfaces;
using MediatR;

namespace Application.Team.Commands.DeleteTeamMember;

public class DeleteTeamMemberCommandHandler : IRequestHandler<DeleteTeamMemberCommand, Result>
{
    private readonly ITeamMemberRepository _teamRepository;
    private readonly IMediaStorageService _mediaStorage;

    public DeleteTeamMemberCommandHandler(ITeamMemberRepository teamRepository, IMediaStorageService mediaStorage)
    {
        _teamRepository = teamRepository;
        _mediaStorage = mediaStorage;
    }

    public async Task<Result> Handle(DeleteTeamMemberCommand request, CancellationToken cancellationToken)
    {
        var entity = await _teamRepository.GetByIdAsync(request.Id, cancellationToken);
        if (entity == null)
            return Result.Failure("TeamMember.NotFound", "Team member not found", 404);

        if (!string.IsNullOrEmpty(entity.ImageUrl))
        {
            await _mediaStorage.DeleteAsync(entity.ImageUrl!, cancellationToken);
        }

        await _teamRepository.DeleteAsync(entity, cancellationToken);
        return Result.Success();
    }
}
