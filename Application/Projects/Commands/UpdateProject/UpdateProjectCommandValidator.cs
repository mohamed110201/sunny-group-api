using FluentValidation;

namespace Application.Projects.Commands.UpdateProject;

public class UpdateProjectCommandValidator : AbstractValidator<UpdateProjectCommand>
{
    public UpdateProjectCommandValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0).WithMessage("Invalid project id");

        RuleFor(x => x.Name.English)
            .NotEmpty()
            .When(x => x.Name != null)
            .WithMessage("English name is required");

        RuleFor(x => x.Description.English)
            .NotEmpty()
            .When(x => x.Description != null)
            .WithMessage("English description is required");


        RuleFor(x => x.YearOfConstruction)
            .GreaterThan(1900).WithMessage("Invalid year")
            .LessThanOrEqualTo(DateTime.UtcNow.Year).WithMessage("Year cannot be in the future")
            .When(x => x.YearOfConstruction.HasValue);

        RuleFor(x => x.NumberOfVillas)
            .GreaterThanOrEqualTo(0).WithMessage("Number of villas cannot be negative")
            .When(x => x.NumberOfVillas.HasValue);

        RuleFor(x => x.NumberOfApartments)
            .GreaterThanOrEqualTo(0).WithMessage("Number of apartments cannot be negative")
            .When(x => x.NumberOfApartments.HasValue);

        RuleFor(x => x.Area)
            .GreaterThan(0).WithMessage("Area must be greater than 0")
            .When(x => x.Area.HasValue);

        RuleFor(x => x.Location.English)
            .NotEmpty()
            .When(x => x.Location != null)
            .WithMessage("English location is required");

        RuleFor(x => x.Status)
            .IsInEnum().WithMessage("Invalid project status")
            .When(x => x.Status.HasValue);

        RuleFor(x => x.Types)
            .NotEmpty().WithMessage("At least one project type is required")
            .When(x => x.Types != null);
        RuleForEach(x => x.Types)
            .IsInEnum().WithMessage("Invalid project type")
            .When(x => x.Types != null);

       


    }
}
