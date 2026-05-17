using Application.Blogs.Dtos;
using Domain.Entities.Blogs;
using Domain.Enums;

namespace Application.Blogs.Mapping;

public static class BlogMappings
{
    public static GetAllBlogsDto ToGetAllDto(this Blog blog) => new()
    {
        Id = blog.Id,
        Title = blog.Title,
        Description = blog.Description,
        ReadTimeMinutes = blog.ReadTimeMinutes,
        MetaTitle =  blog.MetaTitle,
        MetaDescription = blog.MetaDescription,
        Category = blog.Category.Name,
        CreatedAt = blog.CreatedAt,
        Cover = blog.Media
            .Where(m => m.Category == MediaCategory.Cover)
            .Select(m => m.ToDto())
            .FirstOrDefault()
    };

    public static GetBlogByIdDto ToGetByIdDto(this Blog blog) => new()
    {
        Id = blog.Id,
        Title = blog.Title,
        Description = blog.Description,
        ReadTimeMinutes = blog.ReadTimeMinutes,
        Content = blog.Content,
        Category = blog.Category.Name,
        MetaTitle = blog.MetaTitle,
        MetaDescription = blog.MetaDescription,
        CreatedAt = blog.CreatedAt,
        Cover = blog.Media
            .Where(m => m.Category == MediaCategory.Cover)
            .Select(m => m.ToDto())
            .FirstOrDefault(),
        Gallery = blog.Media
            .Where(m => m.Category == MediaCategory.Gallery)
            .Select(m => m.ToDto())
            .ToList()
    };

    private static BlogMediaResponseDto ToDto(this BlogMedia media) => new()
    {
        Id = media.Id,
        Url = media.Url,
        Category = media.Category
    };
 
}
