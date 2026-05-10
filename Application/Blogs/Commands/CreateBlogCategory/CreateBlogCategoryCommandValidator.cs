using Application.Blogs.Commands.CreateBlogCategory;
using FluentValidation;

namespace Application.Blogs.Commands.CreateBlogCategory;

public class CreateBlogCategoryCommandValidator : AbstractValidator<CreateBlogCategoryCommand>
{
    public CreateBlogCategoryCommandValidator()
    {
        RuleFor(x => x.Name.English)
            .NotEmpty().WithMessage("English name is required");
    }
}
