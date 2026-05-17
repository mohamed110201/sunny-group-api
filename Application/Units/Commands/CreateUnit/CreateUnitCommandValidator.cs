using FluentValidation;

namespace Application.Units.Commands.CreateUnit;

public class CreateUnitCommandValidator : AbstractValidator<CreateUnitCommand>
{
    public CreateUnitCommandValidator()
    {
        RuleFor(x => x.ProjectId).GreaterThan(0).WithMessage("Invalid project id");
        RuleFor(x => x.Name.English).NotEmpty().WithMessage("English name is required");
        RuleFor(x => x.MetaTitle.English).MaximumLength(200).When(x => x.MetaTitle?.English != null).WithMessage("Meta title must not exceed 200 characters");
        RuleFor(x => x.MetaDescription.English).MaximumLength(500).When(x => x.MetaDescription?.English != null).WithMessage("Meta description must not exceed 500 characters");
        RuleFor(x => x.Area).GreaterThan(0).WithMessage("Area must be greater than 0");
        RuleFor(x => x.Rooms).GreaterThanOrEqualTo(0).WithMessage("Rooms cannot be negative");
        RuleFor(x => x.NumberOfBathrooms)
            .GreaterThanOrEqualTo(0).WithMessage("Number of bathrooms cannot be negative");
        RuleFor(x => x.Status).IsInEnum().WithMessage("Invalid status");
        RuleFor(x => x.DeliveryCondition).IsInEnum().WithMessage("Invalid delivery condition");

        RuleFor(x => x.Type).IsInEnum().WithMessage("Invalid type");
        RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must be greater than 0");
        RuleFor(x => x.Stock).GreaterThanOrEqualTo(0).WithMessage("Stock cannot be negative");
        RuleFor(x => x.PaymentPlan.English).NotEmpty().WithMessage("English payment plan is required");
        RuleFor(x => x.ROI).GreaterThanOrEqualTo(0).WithMessage("ROI cannot be negative");
    }
}