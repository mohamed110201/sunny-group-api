using Application.Blogs.Commands.CreateBlogCategory;
using FluentValidation;

namespace Application.Blogs.Commands.CreateBlogCategory;

public class CreateBlogCategoryCommandValidator : AbstractValidator<CreateBlogCategoryCommand>
{
    public CreateBlogCategoryCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotNull()
            .WithMessage("Category name is required");

        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("English name is required");
    }
}
