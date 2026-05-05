using FluentValidation;

namespace Application.Home.Commands.UpdateVoiceOfCustomer;

public class UpdateVoiceOfCustomerCommandValidator : AbstractValidator<UpdateVoiceOfCustomerCommand>
{
    public UpdateVoiceOfCustomerCommandValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0).WithMessage("Invalid id");
        RuleFor(x => x.CustomerName)
            .NotEmpty().When(x => x.CustomerName != null).WithMessage("Customer name is required")
            .MaximumLength(100).When(x => x.CustomerName != null).WithMessage("Name must not exceed 100 characters");
        RuleFor(x => x.ProjectId).GreaterThan(0).When(x => x.ProjectId.HasValue).WithMessage("Invalid project id");
        RuleFor(x => x.Comment.English).NotEmpty().When(x => x.Comment != null).WithMessage("English comment is required");
    }
}