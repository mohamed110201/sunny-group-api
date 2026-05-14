using FluentValidation;

namespace Application.Blogs.Commands.UpdateBlog;

public class UpdateBlogCommandValidator : AbstractValidator<UpdateBlogCommand>
{
    public UpdateBlogCommandValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0).WithMessage("Invalid blog id");
        RuleFor(x => x.Title.English).NotEmpty().When(x => x.Title != null).WithMessage("English title is required");
        RuleFor(x => x.Description.English).NotEmpty().When(x => x.Description != null).WithMessage("English description is required");
        RuleFor(x => x.Content.English).NotEmpty().When(x => x.Content != null).WithMessage("English content is required");
        RuleFor(x => x.MetaTitle.English).MaximumLength(200).When(x => x.MetaTitle?.English != null).WithMessage("Meta title must not exceed 200 characters");
        RuleFor(x => x.MetaDescription.English).MaximumLength(500).When(x => x.MetaDescription?.English != null).WithMessage("Meta description must not exceed 500 characters");
        RuleFor(x => x.CategoryId).GreaterThan(0).When(x => x.CategoryId.HasValue).WithMessage("Valid category is required");
    }
}