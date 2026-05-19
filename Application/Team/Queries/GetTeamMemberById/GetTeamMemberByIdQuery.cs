using Application.Common;
using Application.Team.Dtos;
using MediatR;

namespace Application.Team.Queries.GetTeamMemberById;

public record GetTeamMemberByIdQuery(int Id) : IRequest<Result<GetTeamMemberByIdDto>>;
