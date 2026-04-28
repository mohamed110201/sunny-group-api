using FluentValidation;

namespace Application.Units.Commands.CreateUnit;

public class CreateUnitCommandValidator : AbstractValidator<CreateUnitCommand>
{
    public CreateUnitCommandValidator()
    {
        RuleFor(x => x.ProjectId).GreaterThan(0).WithMessage("Invalid project id");
        RuleFor(x => x.Name.English).NotEmpty().WithMessage("English name is required");
        RuleFor(x => x.Area).GreaterThan(0).WithMessage("Area must be greater than 0");
        RuleFor(x => x.Rooms).GreaterThanOrEqualTo(0).WithMessage("Rooms cannot be negative");
        RuleFor(x => x.Status).IsInEnum().WithMessage("Invalid status");
        RuleFor(x => x.Type).IsInEnum().WithMessage("Invalid type");
        RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must be greater than 0");
        RuleFor(x => x.Stock).GreaterThanOrEqualTo(0).WithMessage("Stock cannot be negative");
        RuleFor(x => x.PaymentPlan.English).NotEmpty().WithMessage("English payment plan is required");
        RuleFor(x => x.ROI).GreaterThanOrEqualTo(0).WithMessage("ROI cannot be negative");
    }
}