using Application.Common;
using Domain.ValueObjects;
using MediatR;

namespace Application.Blogs.Commands.CreateBlog;

public class CreateBlogCommand : IRequest<Result<int>>
{
    public LocalizedText Title { get; set; } = null!;
    public LocalizedText Description { get; set; } = null!;
    public LocalizedText Content { get; set; } = null!;
    public LocalizedText MetaTitle { get; set; } = null!;
    public LocalizedText MetaDescription { get; set; } = null!;
    public int CategoryId { get; set; }
}