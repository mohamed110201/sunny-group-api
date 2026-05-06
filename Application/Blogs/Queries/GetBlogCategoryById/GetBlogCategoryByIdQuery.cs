using Application.Blogs.Dtos;
using Application.Common;
using MediatR;

namespace Application.Blogs.Queries.GetBlogCategoryById;

public record GetBlogCategoryByIdQuery(int Id) : IRequest<Result<BlogCategoryResponseDto>>;
