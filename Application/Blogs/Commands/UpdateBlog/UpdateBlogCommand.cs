using Application.Common;
using Domain.Enums;
using Domain.ValueObjects;
using MediatR;

namespace Application.Blogs.Commands.UpdateBlog;

public class UpdateBlogCommand : IRequest<Result>
{
    public int Id { get; set; }
    public LocalizedText? Title { get; set; }
    public LocalizedText? Description { get; set; }
    public LocalizedText? Content { get; set; }
    public string? MetaTitle { get; set; }
    public string? MetaDescription { get; set; }
    public BlogCategory? Category { get; set; }
}