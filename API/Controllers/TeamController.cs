using API.Common;
using Application.Team.Commands.CreateTeamMember;
using Application.Team.Commands.DeleteTeamMember;
using Application.Team.Commands.UpdateTeamMember;
using Application.Team.Queries.GetTeam;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Authorize]
public class TeamController : BaseController
{
    private readonly IMediator _mediator;

    public TeamController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> Get(CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetTeamQuery(), cancellationToken);
        return HandleResult(result);
    }

    [HttpPost]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> Create([FromForm] CreateTeamMemberCommand command, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        return HandleResult(result, created: true);
    }

    [HttpPatch("{id}")]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> Update(int id, [FromForm] UpdateTeamMemberCommand command, CancellationToken cancellationToken)
    {
        command.Id = id;
        var result = await _mediator.Send(command, cancellationToken);
        return HandleResult(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new DeleteTeamMemberCommand(id), cancellationToken);
        return HandleResult(result);
    }
}
