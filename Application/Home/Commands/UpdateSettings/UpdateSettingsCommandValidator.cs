using FluentValidation;

namespace Application.Home.Commands.UpdateSettings;

public class UpdateSettingsCommandValidator : AbstractValidator<UpdateSettingsCommand>
{
    public UpdateSettingsCommandValidator()
    {
        RuleFor(x => x.CompanyFoundingYear)
            .GreaterThan(1900).When(x => x.CompanyFoundingYear.HasValue).WithMessage("Invalid founding year")
            .LessThanOrEqualTo(DateTime.UtcNow.Year).When(x => x.CompanyFoundingYear.HasValue).WithMessage("Founding year cannot be in the future");

        RuleFor(x => x.NumberOfClients)
            .GreaterThanOrEqualTo(0).When(x => x.NumberOfClients.HasValue).WithMessage("Number of clients cannot be negative");
    }
}