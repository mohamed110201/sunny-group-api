using FluentValidation;

namespace Application.Blogs.Commands.CreateBlog;

public class CreateBlogCommandValidator : AbstractValidator<CreateBlogCommand>
{
    public CreateBlogCommandValidator()
    {
        RuleFor(x => x.Title.English).NotEmpty().WithMessage("English title is required");
        RuleFor(x => x.Description.English).NotEmpty().WithMessage("English description is required");
        RuleFor(x => x.ReadTimeMinutes)
            .GreaterThan(0).WithMessage("Read time must be greater than 0");
        RuleFor(x => x.Content.English).NotEmpty().WithMessage("English content is required");
        RuleFor(x => x.MetaTitle.English).MaximumLength(200).When(x => x.MetaTitle?.English != null).WithMessage("Meta title must not exceed 200 characters");
        RuleFor(x => x.MetaDescription.English).MaximumLength(500).When(x => x.MetaDescription?.English != null).WithMessage("Meta description must not exceed 500 characters");
        RuleFor(x => x.CategoryId).GreaterThan(0).WithMessage("Valid category is required");
    }
}
