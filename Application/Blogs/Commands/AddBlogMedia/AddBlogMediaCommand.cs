using Application.Blogs.Dtos;
using Application.Common;
using MediatR;

namespace Application.Blogs.Commands.AddBlogMedia;

public class AddBlogMediaCommand : IRequest<Result>
{
    public int BlogId { get; set; }
    public List<BlogMediaRequestDto> Images { get; set; } = [];
}