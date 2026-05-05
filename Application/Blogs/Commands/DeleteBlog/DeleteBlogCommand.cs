using Application.Common;
using MediatR;

namespace Application.Blogs.Commands.DeleteBlog;

public record DeleteBlogCommand(int Id) : IRequest<Result>;