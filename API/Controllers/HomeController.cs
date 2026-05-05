using API.Common;
using Application.Home.Commands.CreateVoiceOfCustomer;
using Application.Home.Commands.DeleteVoiceOfCustomer;
using Application.Home.Commands.UpdateHomeMedia;
using Application.Home.Commands.UpdateSettings;
using Application.Home.Commands.UpdateVoiceOfCustomer;
using Application.Home.Queries.GetHome;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class HomeController : BaseController
{
    private readonly IMediator _mediator;

    public HomeController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> Get(CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetHomeQuery(), cancellationToken);
        return HandleResult(result);
    }

    [HttpPut("media")]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> UpdateMedia(
        [FromForm] UpdateHomeMediaCommand command,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        return HandleResult(result);
    }

    [HttpPut("settings")]
    public async Task<IActionResult> UpdateSettings(
        [FromBody] UpdateSettingsCommand command,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        return HandleResult(result);
    }

    [HttpPost("voice-of-customers")]
    public async Task<IActionResult> CreateVoiceOfCustomer(
        [FromBody] CreateVoiceOfCustomerCommand command,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        return HandleResult(result, created:true);
    }

    [HttpPut("voice-of-customers/{id}")]
    public async Task<IActionResult> UpdateVoiceOfCustomer(
        int id,
        [FromBody] UpdateVoiceOfCustomerCommand command,
        CancellationToken cancellationToken)
    {
        command.Id = id;
        var result = await _mediator.Send(command, cancellationToken);
        return HandleResult(result);
    }

    [HttpDelete("voice-of-customers/{id}")]
    public async Task<IActionResult> DeleteVoiceOfCustomer(
        int id,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(
            new DeleteVoiceOfCustomerCommand(id), cancellationToken);
        return HandleResult(result);
    }
}