using API.Common;
using Application.Blogs.Commands.AddBlogMedia;
using Application.Blogs.Commands.CreateBlog;
using Application.Blogs.Commands.DeleteBlog;
using Application.Blogs.Commands.DeleteBlogMedia;
using Application.Blogs.Commands.UpdateBlog;
using Application.Blogs.Queries.GetAllBlogs;
using Application.Blogs.Queries.GetBlogById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class BlogsController : BaseController
{
    private readonly IMediator _mediator;

    public BlogsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetAllBlogsQuery(), cancellationToken);
        return HandleResult(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetBlogByIdQuery(id), cancellationToken);
        return HandleResult(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] CreateBlogCommand command,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        return HandleResult(result, created: true);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(
        int id,
        [FromBody] UpdateBlogCommand command,
        CancellationToken cancellationToken)
    {
        command.Id = id;
        var result = await _mediator.Send(command, cancellationToken);
        return HandleResult(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new DeleteBlogCommand(id), cancellationToken);
        return HandleResult(result);
    }

    [HttpPost("{id}/media")]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> AddMedia(
        int id,
        [FromForm] AddBlogMediaCommand command,
        CancellationToken cancellationToken)
    {
        command.BlogId = id;
        var result = await _mediator.Send(command, cancellationToken);
        return HandleResult(result);
    }

    [HttpDelete("{id}/media/{mediaId}")]
    public async Task<IActionResult> DeleteMedia(
        int id, int mediaId,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(
            new DeleteBlogMediaCommand(id, mediaId), cancellationToken);
        return HandleResult(result);
    }
}