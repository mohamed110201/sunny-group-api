using Application.Common.Options;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Options;
using PhoneNumbers;

namespace Application.Team.Commands.UpdateTeamMember;

public class UpdateTeamMemberCommandValidator : AbstractValidator<UpdateTeamMemberCommand>
{
    public UpdateTeamMemberCommandValidator(IOptions<MediaValidationOptions> options)
    {
        var settings = options.Value;
        var maxSizeBytes = settings.MaxImageSizeMb * 1024 * 1024;

        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("Invalid team member id");

        When(x => x.Name != null, () =>
        {
            RuleFor(x => x.Name!.English)
                .NotEmpty().WithMessage("English name is required");
        });

        When(x => x.Position != null, () =>
        {
            RuleFor(x => x.Position!.English)
                .NotEmpty().WithMessage("English position is required");
        });

        When(x => x.Department != null, () =>
        {
            RuleFor(x => x.Department!.English)
                .NotEmpty().WithMessage("English department is required");
        });

        When(x => x.Image != null, () =>
        {
            RuleFor(x => x.Image!)
                .Must(f => f.Length > 0).WithMessage("Image is empty")
                .Must(f => f.Length <= maxSizeBytes)
                .WithMessage($"Image must not exceed {settings.MaxImageSizeMb}MB")
                .Must(f => settings.AllowedImageTypes.Contains(f.ContentType.ToLower()))
                .WithMessage($"Allowed types: {string.Join(", ", settings.AllowedImageTypes)}");
        });

        When(x => !string.IsNullOrEmpty(x.Email), () =>
        {
            RuleFor(x => x.Email!)
                .EmailAddress().WithMessage("Invalid email format")
                .MaximumLength(200).WithMessage("Email must not exceed 200 characters");
        });

        When(x => !string.IsNullOrEmpty(x.CountryCode) || !string.IsNullOrEmpty(x.PhoneNumber), () =>
        {
            RuleFor(x => x.CountryCode)
                .NotEmpty().WithMessage("Country code is required when phone number is provided");

            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("Phone number is required when country code is provided");

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
                        { ErrorCode = "Phone.InvalidForCountry" });
                    }
                });
        });
    }
}