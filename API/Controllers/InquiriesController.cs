using API.Common;
using Application.Inquiries.Commands.CreateInquiry;
using Application.Inquiries.Commands.MarkInquiryAsSeen;
using Application.Inquiries.Queries.GetAllInquiries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class InquiriesController : BaseController
{
    private readonly IMediator _mediator;

    public InquiriesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetAllInquiriesQuery(), cancellationToken);
        return HandleResult(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] CreateInquiryCommand command,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        return HandleResult(result,created:true);    
    }

    [HttpPatch("{id}/seen")]
    public async Task<IActionResult> MarkAsSeen(int id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(
            new MarkInquiryAsSeenCommand(id), cancellationToken);
        return HandleResult(result);
    }
}