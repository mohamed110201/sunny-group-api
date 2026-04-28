using API.Common;
using API.Common.JSend;
using API.Extensions;
using Application.Projects.Commands.AddAttraction;
using Application.Projects.Commands.AddProjectMedia;
using Application.Projects.Commands.CreateProject;
using Application.Projects.Commands.DeleteAttraction;
using Application.Projects.Commands.DeleteProject;
using Application.Projects.Commands.DeleteProjectMedia;
using Application.Projects.Commands.UpdateAttraction;
using Application.Projects.Commands.UpdateProject;
using Application.Projects.Dtos;
using Application.Projects.Queries.GetProjectById;
using Application.Projects.Queries.GetProjects;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class ProjectsController : BaseController
{
    private readonly IMediator _mediator;

    public ProjectsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetAllProjectsQuery(), cancellationToken);
        return HandleResult(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetProjectByIdQuery(id), cancellationToken);
        return HandleResult(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] CreateProjectCommand command,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        return HandleResult(result, created: true);    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(
        int id,
        [FromBody] UpdateProjectCommand command,
        CancellationToken cancellationToken)
    {
        command.Id = id;
        var result = await _mediator.Send(command, cancellationToken);
        return HandleResult(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new DeleteProjectCommand(id), cancellationToken);
        return HandleResult(result);
    }

    [HttpPost("{id}/media")]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> AddMedia(
        int id,
        [FromForm] AddProjectMediaForm form,
        CancellationToken cancellationToken)
    {
        if (form.Images.Count != form.Categories.Count)
        {
            return JSendHelper.BadRequest(
                error : "Images and Categories count must match."
            );
        }
        var result = await _mediator.Send(form.ToCommand(id), cancellationToken);
        return HandleResult(result);
    }

    [HttpDelete("{id}/media/{mediaId}")]
    public async Task<IActionResult> DeleteMedia(
        int id, int mediaId,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(
            new DeleteProjectMediaCommand(id, mediaId), cancellationToken);
        return HandleResult(result);
    }

    [HttpPost("{id}/attractions")]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> AddAttractions(
        int id,
        [FromForm] AddAttractionCommand command,
        CancellationToken cancellationToken)
    {
        command.ProjectId = id;
        var result = await _mediator.Send(command, cancellationToken);
        return HandleResult(result);
    }

    [HttpPut("{id}/attractions/{attractionId}")]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> UpdateAttraction(
        int id, int attractionId,
        [FromForm] UpdateAttractionCommand command,
        CancellationToken cancellationToken)
    {
        command.Id = attractionId;
        command.ProjectId = id;
        var result = await _mediator.Send(command, cancellationToken);
        return HandleResult(result);
    }

    [HttpDelete("{id}/attractions/{attractionId}")]
    public async Task<IActionResult> DeleteAttraction(
        int id, int attractionId,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(
            new DeleteAttractionCommand(id, attractionId), cancellationToken);
        return HandleResult(result);
    }
}
