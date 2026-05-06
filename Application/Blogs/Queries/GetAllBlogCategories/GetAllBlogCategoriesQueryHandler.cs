using Application.Blogs.Dtos;
using Application.Blogs.Mapping;
using Application.Common;
using Domain.Interfaces;
using MediatR;

namespace Application.Blogs.Queries.GetAllBlogCategories;

public class GetAllBlogCategoriesQueryHandler : IRequestHandler<GetAllBlogCategoriesQuery, Result<IEnumerable<BlogCategoryResponseDto>>>
{
    private readonly IBlogCategoryRepository _categoryRepository;

    public GetAllBlogCategoriesQueryHandler(IBlogCategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<Result<IEnumerable<BlogCategoryResponseDto>>> Handle(
        GetAllBlogCategoriesQuery request,
        CancellationToken cancellationToken)
    {
        var categories = await _categoryRepository.GetAllAsync(cancellationToken);
        var dtos = categories.Select(c => c.ToDto());
        return Result<IEnumerable<BlogCategoryResponseDto>>.Success(dtos);
    }
}
