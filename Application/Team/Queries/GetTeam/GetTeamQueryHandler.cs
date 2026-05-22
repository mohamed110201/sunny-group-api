using Application.Common;
using Application.Team.Dtos;
using Domain.Interfaces;
using MediatR;

namespace Application.Team.Queries.GetTeam;

public class GetTeamQueryHandler : IRequestHandler<GetTeamQuery, Result<List<TeamMemberDto>>>
{
    private readonly ITeamMemberRepository _teamRepository;

    public GetTeamQueryHandler(ITeamMemberRepository teamRepository)
    {
        _teamRepository = teamRepository;
    }

    public async Task<Result<List<TeamMemberDto>>> Handle(GetTeamQuery request, CancellationToken cancellationToken)
    {
        var items = await _teamRepository.GetAllAsync(cancellationToken);
        var dtos = items.Select(i => new TeamMemberDto
        {
            Id = i.Id,
            Name = i.Name,
            ImageUrl = i.ImageUrl,
            Position = i.Position,
            Department = i.Department,
            PhoneNumber = i.PhoneNumber,
            Email = i.Email,
            Links = i.Links
        }).ToList();

        return Result<List<TeamMemberDto>>.Success(dtos);
    }
}
