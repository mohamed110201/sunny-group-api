using FluentValidation;
using FluentValidation.Results;
using PhoneNumbers;

namespace Application.Inquiries.Commands.CreateInquiry;

public class CreateInquiryCommandValidator : AbstractValidator<CreateInquiryCommand>
{
    public CreateInquiryCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required")
            .MaximumLength(100).WithMessage("Name must not exceed 100 characters");

        RuleFor(x => x.CountryCode)
            .NotEmpty().WithMessage("Country code is required");

        RuleFor(x => x.PhoneNumber)
            .NotEmpty().WithMessage("Phone number is required");
        
        RuleFor(x => x)
            .Custom((x, context) =>
            {
                if (string.IsNullOrEmpty(x.CountryCode) || string.IsNullOrEmpty(x.PhoneNumber))
                    return;

                var phoneUtil = PhoneNumberUtil.GetInstance();
                PhoneNumbers.PhoneNumber parsed;

                try
                {
                    parsed = phoneUtil.Parse($"{x.CountryCode}{x.PhoneNumber}", null);
                }
                catch (NumberParseException ex)
                {
                    var (code, message) = ex.ErrorType switch
                    {
                        ErrorType.INVALID_COUNTRY_CODE => ("Phone.InvalidCountryCode", "Invalid country code"),
                        ErrorType.NOT_A_NUMBER         => ("Phone.NotANumber", "Value entered is not a phone number"),
                        ErrorType.TOO_SHORT_AFTER_IDD  => ("Phone.TooShort", "Phone number is too short after country code"),
                        ErrorType.TOO_SHORT_NSN        => ("Phone.TooShortForCountry", "Phone number is too short for this country"),
                        ErrorType.TOO_LONG             => ("Phone.TooLong", "Phone number is too long"),
                        _                              => ("Phone.Invalid", "Could not parse phone number")
                    };
                    context.AddFailure(new ValidationFailure("PhoneNumber", message) { ErrorCode = code });
                    return;
                }

                if (!phoneUtil.IsValidNumber(parsed))
                {
                    var region = phoneUtil.GetRegionCodeForNumber(parsed);
                    context.AddFailure(new ValidationFailure("PhoneNumber",
                        string.IsNullOrEmpty(region)
                            ? "Phone number is not valid"
                            : $"Phone number is not valid for {region}")
                    {
                        ErrorCode = "Phone.InvalidForCountry"
                    });
                }
            });

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required")
            .EmailAddress().WithMessage("Invalid email address");

        RuleFor(x => x.Message)
            .NotEmpty().WithMessage("Message is required")
            .MaximumLength(1000).WithMessage("Message must not exceed 1000 characters");
    }
}