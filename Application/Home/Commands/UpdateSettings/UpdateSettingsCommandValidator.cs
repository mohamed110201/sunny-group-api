using FluentValidation;

namespace Application.Home.Commands.UpdateSettings;

public class UpdateSettingsCommandValidator : AbstractValidator<UpdateSettingsCommand>
{
    public UpdateSettingsCommandValidator()
    {
        RuleFor(x => x.CompanyFoundingYear)
            .GreaterThan(1900).WithMessage("Invalid founding year")
            .LessThanOrEqualTo(DateTime.UtcNow.Year).WithMessage("Founding year cannot be in the future");

        RuleFor(x => x.NumberOfClients)
            .GreaterThanOrEqualTo(0).WithMessage("Number of clients cannot be negative");
    }
}