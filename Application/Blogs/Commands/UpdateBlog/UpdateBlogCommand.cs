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
    public int? ReadTimeMinutes { get; set; }

    public LocalizedText? Content { get; set; }
    public LocalizedText? MetaTitle { get; set; }
    public LocalizedText? MetaDescription { get; set; }
    public int? CategoryId { get; set; }
}