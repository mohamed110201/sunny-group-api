using FluentValidation;

namespace Application.Projects.Commands.UpdateProject;

    public class UpdateProjectCommandValidator : AbstractValidator<UpdateProjectCommand>
    {
        public UpdateProjectCommandValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0).WithMessage("Invalid project id");

            RuleFor(x => x.Name.English)
                .NotEmpty().WithMessage("English name is required");

            RuleFor(x => x.Description.English)
                .NotEmpty().WithMessage("English description is required");
        

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

            RuleFor(x => x.PriceRange)
                .NotNull().WithMessage("Price range is required");
            RuleFor(x => x.PriceRange.Min)
                .GreaterThanOrEqualTo(0).WithMessage("Minimum price cannot be negative");
            RuleFor(x => x.PriceRange.Max)
                .GreaterThan(x => x.PriceRange.Min).WithMessage("Maximum price must be greater than minimum price");


        }
    }
