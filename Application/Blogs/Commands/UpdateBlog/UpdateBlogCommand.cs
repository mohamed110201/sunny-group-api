using Application.Common;
using Domain.Enums;
using Domain.ValueObjects;
using MediatR;

namespace Application.Blogs.Commands.UpdateBlog;

public class UpdateBlogCommand : IRequest<Result>
{
    public int Id { get; set; }
    public LocalizedText Title { get; set; } = null!;
    public LocalizedText Description { get; set; } = null!;
    public LocalizedText Content { get; set; } = null!;
    public string MetaTitle { get; set; } = string.Empty;
    public string MetaDescription { get; set; } = string.Empty;
    public BlogCategory Category { get; set; }
}