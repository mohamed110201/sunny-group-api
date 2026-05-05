using FluentValidation;

namespace Application.Units.Commands.UpdateUnit;

public class UpdateUnitCommandValidator : AbstractValidator<UpdateUnitCommand>
{
    public UpdateUnitCommandValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0).WithMessage("Invalid unit id");
        RuleFor(x => x.ProjectId).GreaterThan(0).WithMessage("Invalid project id");
        RuleFor(x => x.Name.English).NotEmpty().When(x => x.Name != null).WithMessage("English name is required");
        RuleFor(x => x.Area).GreaterThan(0).When(x => x.Area.HasValue).WithMessage("Area must be greater than 0");
        RuleFor(x => x.Rooms).GreaterThanOrEqualTo(0).When(x => x.Rooms.HasValue).WithMessage("Rooms cannot be negative");
        RuleFor(x => x.Status).IsInEnum().When(x => x.Status.HasValue).WithMessage("Invalid status");
        RuleFor(x => x.Type).IsInEnum().When(x => x.Type.HasValue).WithMessage("Invalid type");
        RuleFor(x => x.Price).GreaterThan(0).When(x => x.Price.HasValue).WithMessage("Price must be greater than 0");
        RuleFor(x => x.Stock).GreaterThanOrEqualTo(0).When(x => x.Stock.HasValue).WithMessage("Stock cannot be negative");
        RuleFor(x => x.PaymentPlan.English).NotEmpty().When(x => x.PaymentPlan != null).WithMessage("English payment plan is required");
        RuleFor(x => x.ROI).GreaterThanOrEqualTo(0).When(x => x.ROI.HasValue).WithMessage("ROI cannot be negative");
    }
}