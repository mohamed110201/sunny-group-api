using Application.Common;
using Domain.ValueObjects;
using MediatR;

namespace Application.Blogs.Commands.UpdateBlogCategory;

public class UpdateBlogCategoryCommand : IRequest<Result>
{
    public int Id { get; set; }
    public string? Name { get; set; }
}
