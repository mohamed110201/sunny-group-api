using Application.Common;
using Application.Common.Interfaces;
using Domain.Entities.Team;
using Domain.Interfaces;
using MediatR;

namespace Application.Team.Commands.UpdateTeamMember;

public class UpdateTeamMemberCommandHandler : IRequestHandler<UpdateTeamMemberCommand, Result>
{
    private readonly ITeamMemberRepository _teamRepository;
    private readonly IMediaStorageService _mediaStorage;

    public UpdateTeamMemberCommandHandler(
        ITeamMemberRepository teamRepository,
        IMediaStorageService mediaStorage)
    {
        _teamRepository = teamRepository;
        _mediaStorage = mediaStorage;
    }

    public async Task<Result> Handle(UpdateTeamMemberCommand request, CancellationToken cancellationToken)
    {
        var entity = await _teamRepository.GetByIdAsync(request.Id, cancellationToken);
        if (entity == null)
            return Result.Failure("TeamMember.NotFound", "Team member not found", 404);

        if (request.RemoveImage)
        {
            if (!string.IsNullOrEmpty(entity.ImageUrl))
                await _mediaStorage.DeleteAsync(entity.ImageUrl!, cancellationToken);

            entity.ImageUrl = string.Empty;
        }
        else if (request.Image != null)
        {
            // delete old image if exists
            if (!string.IsNullOrEmpty(entity.ImageUrl))
            {
                await _mediaStorage.DeleteAsync(entity.ImageUrl!, cancellationToken);
            }

            entity.ImageUrl = await _mediaStorage.SaveAsync(request.Image, "team-members", cancellationToken);
        }

        if (request.Name != null)
            entity.Name = request.Name;
        if (request.Position != null)
            entity.Position = request.Position;
        if (request.Department != null)
            entity.Department = request.Department;
        if (request.PhoneNumber != null)
            entity.PhoneNumber = request.PhoneNumber;
        if (request.Email != null)
            entity.Email = request.Email;
        if (request.Links != null)
            entity.Links = request.Links;

        await _teamRepository.UpdateAsync(entity, cancellationToken);
        return Result.Success();
    }
}
