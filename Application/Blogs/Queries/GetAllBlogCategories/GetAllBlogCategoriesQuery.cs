using Application.Common;
using Application.Common.Behaviors;
using MediatR;

namespace Application.Blogs.Queries.GetAllBlogCategories;

public record GetAllBlogCategoriesQuery : IRequest<Result<IEnumerable<Application.Blogs.Dtos.BlogCategoryResponseDto>>>, ICacheable
{
    public string CacheKey => "blog-categories";
    public TimeSpan? CacheDuration => TimeSpan.FromHours(1);
}
