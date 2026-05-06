using Application.Blogs.Dtos;
using Application.Blogs.Mapping;
using Application.Common;
using Domain.Interfaces;
using MediatR;

namespace Application.Blogs.Queries.GetBlogCategoryById;

public class GetBlogCategoryByIdQueryHandler : IRequestHandler<GetBlogCategoryByIdQuery, Result<BlogCategoryResponseDto>>
{
    private readonly IBlogCategoryRepository _categoryRepository;

    public GetBlogCategoryByIdQueryHandler(IBlogCategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<Result<BlogCategoryResponseDto>> Handle(
        GetBlogCategoryByIdQuery request,
        CancellationToken cancellationToken)
    {
        var category = await _categoryRepository.GetByIdAsync(request.Id, cancellationToken);
        if (category == null)
            return Result<BlogCategoryResponseDto>.Failure("Category.NotFound","Category not found or is inactive",404);

        return Result<BlogCategoryResponseDto>.Success(category.ToDto());
    }
}
