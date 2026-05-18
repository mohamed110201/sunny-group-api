using API.Common;
using Application.Blogs.Commands.AddBlogMedia;
using Application.Blogs.Commands.CreateBlog;
using Application.Blogs.Commands.CreateBlogCategory;
using Application.Blogs.Commands.DeleteBlog;
using Application.Blogs.Commands.DeleteBlogCategory;
using Application.Blogs.Commands.DeleteBlogMedia;
using Application.Blogs.Commands.UpdateBlog;
using Application.Blogs.Commands.UpdateBlogCategory;
using Application.Blogs.Commands.UploadBlogContentImage;
using Application.Blogs.Queries.GetAllBlogs;
using Application.Blogs.Queries.GetAllBlogCategories;
using Application.Blogs.Queries.GetBlogById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Authorize]
public class BlogsController : BaseController
{
    private readonly IMediator _mediator;

    public BlogsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    #region Categories

    [HttpGet("categories")]
    [AllowAnonymous]
    public async Task<IActionResult> GetAllCategories(CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetAllBlogCategoriesQuery(), cancellationToken);
        return HandleResult(result);
    }

    [HttpPost("categories")]
    public async Task<IActionResult> CreateCategory(
        [FromBody] CreateBlogCategoryCommand command,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        return HandleResult(result, created: true);
    }

    [HttpPatch("categories/{id}")]
    public async Task<IActionResult> UpdateCategory(
        int id,
        [FromBody] UpdateBlogCategoryCommand command,
        CancellationToken cancellationToken)
    {
        command.Id = id;
        var result = await _mediator.Send(command, cancellationToken);
        return HandleResult(result);
    }

    [HttpDelete("categories/{id}")]
    public async Task<IActionResult> DeleteCategory(int id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new DeleteBlogCategoryCommand(id), cancellationToken);
        return HandleResult(result);
    }

    #endregion

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetAllBlogsQuery(), cancellationToken);
        return HandleResult(result);
    }

    [HttpGet("{id}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetBlogByIdQuery(id), cancellationToken);
        return HandleResult(result);
    }

    [HttpPost]
    [AllowAnonymous]

    public async Task<IActionResult> Create(
        [FromBody] CreateBlogCommand command,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        return HandleResult(result, created: true);
    }

    [HttpPost("content-image")]
    [AllowAnonymous]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> UploadContentImage(
        IFormFile file,
        CancellationToken cancellationToken)
    {
        var command = new UploadBlogContentImageCommand(file);
        var result = await _mediator.Send(command, cancellationToken);

        if (!result.IsSuccess)
            return BadRequest(result.Errors);

        return Ok(new { location = result.Value });
    }

    [HttpPatch("{id}")]
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