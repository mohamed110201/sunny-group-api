using Application.Common;
using Application.Team.Dtos;
using MediatR;

namespace Application.Team.Queries.GetTeam;

public record GetTeamQuery : IRequest<Result<List<TeamMemberDto>>>;
