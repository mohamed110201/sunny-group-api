using Application.Common;
using Application.Common.Interfaces;
using Domain.Entities.Team;
using Domain.Interfaces;
using MediatR;

namespace Application.Team.Commands.CreateTeamMember;

public class CreateTeamMemberCommandHandler : IRequestHandler<CreateTeamMemberCommand, Result>
{
    private readonly ITeamMemberRepository _teamRepository;
    private readonly IMediaStorageService _mediaStorage;

    public CreateTeamMemberCommandHandler(
        ITeamMemberRepository teamRepository,
        IMediaStorageService mediaStorage)
    {
        _teamRepository = teamRepository;
        _mediaStorage = mediaStorage;
    }

    public async Task<Result> Handle(CreateTeamMemberCommand request, CancellationToken cancellationToken)
    {
        string? imageUrl = null;
        if (request.Image != null)
        {
            imageUrl = await _mediaStorage.SaveAsync(request.Image, "team-members", cancellationToken);
        }

        var entity = new TeamMember
        {
            Name = request.Name,
            ImageUrl = imageUrl,
            Position = request.Position,
            Department = request.Department,
            PhoneNumber = request.PhoneNumber,
            Email = request.Email,
            Links = request.Links
        };

        await _teamRepository.AddAsync(entity, cancellationToken);
        return Result.Success();
    }
}
