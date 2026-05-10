using Application.Blogs.Dtos;
using Application.Common;
using Domain.ValueObjects;
using MediatR;

namespace Application.Blogs.Commands.CreateBlogCategory;

public class CreateBlogCategoryCommand : IRequest<Result<int>>
{
    public LocalizedText Name { get; set; } = null!;
}
