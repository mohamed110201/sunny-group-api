using Application.Blogs.Commands.UpdateBlogCategory;
using FluentValidation;

namespace Application.Blogs.Commands.UpdateBlogCategory;

public class UpdateBlogCategoryCommandValidator : AbstractValidator<UpdateBlogCategoryCommand>
{
    public UpdateBlogCategoryCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0)
            .WithMessage("Category ID must be greater than 0");

        When(x => x.Name != null, () =>
        {
            RuleFor(x => x.Name!)
                .NotEmpty()
                .WithMessage("English name is required");
        });
    }
}
