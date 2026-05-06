using Application.Common;
using MediatR;

namespace Application.Blogs.Commands.DeleteBlogCategory;

public class DeleteBlogCategoryCommand : IRequest<Result>
{
    public int Id { get; set; }

    public DeleteBlogCategoryCommand(int id)
    {
        Id = id;
    }
}
