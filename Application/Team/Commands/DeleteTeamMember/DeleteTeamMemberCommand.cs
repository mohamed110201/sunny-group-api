using Application.Common;
using MediatR;

namespace Application.Team.Commands.DeleteTeamMember;

public record DeleteTeamMemberCommand(int Id) : IRequest<Result>;
