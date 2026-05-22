using Application.Common.Options;
using FluentValidation;
using Microsoft.Extensions.Options;
using System.Text.RegularExpressions;

namespace Application.Team.Commands.CreateTeamMember;

public class CreateTeamMemberCommandValidator : AbstractValidator<CreateTeamMemberCommand>
{
    public CreateTeamMemberCommandValidator(IOptions<MediaValidationOptions> options)
    {
        var settings = options.Value;
        var maxSizeBytes = settings.MaxImageSizeMb * 1024 * 1024;

        RuleFor(x => x.Name.English)
            .NotEmpty().WithMessage("English name is required");

        RuleFor(x => x.Position.English)
            .NotEmpty().WithMessage("English position is required");

        RuleFor(x => x.Department.English)
            .NotEmpty().WithMessage("English department is required");

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

        When(x => !string.IsNullOrEmpty(x.PhoneNumber), () =>
        {
            RuleFor(x => x.PhoneNumber!)
                .Matches(@"^(?:01[0-9]{9}|\+[1-9]\d{1,14})$")
                .WithMessage("Enter A valid phone number");
        });
    }
}