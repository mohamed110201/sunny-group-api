using Application.Units.Commands.AddUnitMedia;
using Application.Units.Commands.CreateUnit;
using Application.Units.Commands.DeleteUnit;
using Application.Units.Commands.DeleteUnitMedia;
using Application.Units.Commands.UpdateUnit;
using Application.Units.Queries.GetUnitById;
using Application.Units.Queries.GetUnitsByProjectId;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using API.Common;
using API.Common.JSend;
using Application.Units.Commands.AddUnitDocuments;
using Application.Units.Commands.DeleteUnitDocument;
using Application.Units.Dtos;
using Application.Units.Queries.GetAllUnits;

namespace API.Controllers;

[Authorize]
[Route("projects/{projectId}/units")]
public class UnitsController : BaseController
{
    private readonly IMediator _mediator;

    public UnitsController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpGet("/units")]
    [AllowAnonymous]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetAllUnitsQuery(), cancellationToken);
        return HandleResult(result);
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> GetAllByProjectId(int projectId, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetUnitsByProjectIdQuery(projectId), cancellationToken);
        return HandleResult(result);
    }

    [HttpGet("{unitId}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetById(int projectId, int unitId, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetUnitByIdQuery(projectId, unitId), cancellationToken);
        return HandleResult(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        int projectId,
        [FromBody] CreateUnitCommand command,
        CancellationToken cancellationToken)
    {
        command.ProjectId = projectId;
        var result = await _mediator.Send(command, cancellationToken);
        return HandleResult(result, created: true);
    }

    [HttpPatch("{unitId}")]
    public async Task<IActionResult> Update(
        int projectId,
        int unitId,
        [FromBody] UpdateUnitCommand command,
        CancellationToken cancellationToken)
    {
        command.Id = unitId;
        command.ProjectId = projectId;
        var result = await _mediator.Send(command, cancellationToken);
        return HandleResult(result);
    }

    [HttpDelete("{unitId}")]
    public async Task<IActionResult> Delete(
        int projectId,
        int unitId,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new DeleteUnitCommand(projectId, unitId), cancellationToken);
        return HandleResult(result);
    }

    [HttpPost("{unitId}/media")]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> AddMedia(
        int projectId,
        int unitId,
        [FromForm] AddUnitMediaCommand command,
        CancellationToken cancellationToken)
    {

        command.ProjectId = projectId;
        command.UnitId = unitId;
        var result = await _mediator.Send(command);
        return HandleResult(result);
    }

    [HttpDelete("{unitId}/media/{mediaId}")]
    public async Task<IActionResult> DeleteMedia(
        int projectId,
        int unitId,
        int mediaId,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(
            new DeleteUnitMediaCommand(projectId, unitId, mediaId), cancellationToken);
        return HandleResult(result);
    }
    [HttpPost("{unitId}/documents")]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> AddDocuments(
        int projectId,
        int unitId,
        [FromForm] AddUnitDocumentsCommand command,
        CancellationToken cancellationToken)
    {
        command.UnitId = unitId;
        command.ProjectId = projectId;
        var result = await _mediator.Send(command, cancellationToken);
        return HandleResult(result);
    }

    [HttpDelete("{unitId}/documents/{documentId}")]
    public async Task<IActionResult> DeleteDocument(
        int projectId,
        int unitId,
        int documentId,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(
            new DeleteUnitDocumentCommand(projectId, unitId, documentId), cancellationToken);
        return HandleResult(result);
    }

}