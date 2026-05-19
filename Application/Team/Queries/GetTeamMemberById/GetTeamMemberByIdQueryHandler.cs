using Application.Common;
using Application.Team.Dtos;
using Domain.Interfaces;
using MediatR;

namespace Application.Team.Queries.GetTeamMemberById;

public class GetTeamMemberByIdQueryHandler : IRequestHandler<GetTeamMemberByIdQuery, Result<GetTeamMemberByIdDto>>
{
    private readonly ITeamMemberRepository _teamRepository;

    public GetTeamMemberByIdQueryHandler(ITeamMemberRepository teamRepository)
    {
        _teamRepository = teamRepository;
    }

    public async Task<Result<GetTeamMemberByIdDto>> Handle(GetTeamMemberByIdQuery request, CancellationToken cancellationToken)
    {
        var item = await _teamRepository.GetByIdAsync(request.Id, cancellationToken);

        if (item is null)
            return Result<GetTeamMemberByIdDto>.Failure("TeamMember.NotFound", "Team member not found", 404);

        var dto = new GetTeamMemberByIdDto
        {
            Id = item.Id,
            Name = item.Name,
            ImageUrl = item.ImageUrl,
            Position = item.Position,
            Department = item.Department,
            FullPhoneNumber = item.FullPhoneNumber,
            Email = item.Email,
            Links = item.Links
        };

        return Result<GetTeamMemberByIdDto>.Success(dto);
    }
}
