using FluentValidation;

namespace Application.Projects.Commands.CreateProject;

public class CreateProjectCommandValidator : AbstractValidator<CreateProjectCommand>
{
    public CreateProjectCommandValidator()
    {
        RuleFor(x => x.Name.English)
            .NotEmpty().WithMessage("English name is required");

        RuleFor(x => x.Description.English)
            .NotEmpty().WithMessage("English description is required");

        RuleFor(x => x.MetaTitle.English)
            .MaximumLength(200).When(x => x.MetaTitle?.English != null).WithMessage("Meta title must not exceed 200 characters");

        RuleFor(x => x.MetaDescription.English)
            .MaximumLength(500).When(x => x.MetaDescription?.English != null).WithMessage("Meta description must not exceed 500 characters");

        RuleFor(x => x.YearOfConstruction)
            .GreaterThan(1900).WithMessage("Invalid year")
            .LessThanOrEqualTo(DateTime.UtcNow.Year).WithMessage("Year cannot be in the future");

        RuleFor(x => x.NumberOfVillas)
            .GreaterThanOrEqualTo(0).WithMessage("Number of villas cannot be negative");

        RuleFor(x => x.NumberOfApartments)
            .GreaterThanOrEqualTo(0).WithMessage("Number of apartments cannot be negative");

        RuleFor(x => x.Area)
            .GreaterThan(0).WithMessage("Area must be greater than 0");

        RuleFor(x => x.Location.English).NotEmpty().WithMessage("English location is required");

        RuleFor(x => x.Status)
            .IsInEnum().WithMessage("Invalid project status");

        RuleFor(x => x.Types)
            .NotEmpty().WithMessage("At least one project type is required");
        RuleForEach(x => x.Types)
            .IsInEnum().WithMessage("Invalid project type");


    }

}