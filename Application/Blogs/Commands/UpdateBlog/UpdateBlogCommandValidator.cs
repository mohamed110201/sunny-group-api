using FluentValidation;

namespace Application.Blogs.Commands.UpdateBlog;

public class UpdateBlogCommandValidator : AbstractValidator<UpdateBlogCommand>
{
    public UpdateBlogCommandValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0).WithMessage("Invalid blog id");
        RuleFor(x => x.Title.English).NotEmpty().WithMessage("English title is required");
        RuleFor(x => x.Description.English).NotEmpty().WithMessage("English description is required");
        RuleFor(x => x.Content.English).NotEmpty().WithMessage("English content is required");
        RuleFor(x => x.MetaTitle).NotEmpty().WithMessage("Meta title is required")
            .MaximumLength(200).WithMessage("Meta title must not exceed 200 characters");
        RuleFor(x => x.MetaDescription).NotEmpty().WithMessage("Meta description is required")
            .MaximumLength(500).WithMessage("Meta description must not exceed 500 characters");
        RuleFor(x => x.Category).IsInEnum().WithMessage("Invalid category");
    }
}