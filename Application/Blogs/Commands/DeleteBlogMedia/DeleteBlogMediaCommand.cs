using Application.Common;
using MediatR;

namespace Application.Blogs.Commands.DeleteBlogMedia;

public record DeleteBlogMediaCommand(int BlogId, int MediaId) : IRequest<Result>;