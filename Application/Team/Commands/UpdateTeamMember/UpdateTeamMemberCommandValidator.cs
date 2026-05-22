using Application.Common.Options;
using FluentValidation;
using Microsoft.Extensions.Options;
using System.Text.RegularExpressions;

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

        When(x => !string.IsNullOrEmpty(x.PhoneNumber), () =>
        {
            RuleFor(x => x.PhoneNumber!)
                .Matches(@"^01[0-9]{9}$")
                .WithMessage("Enter a valid phone number ");
        });
    }
}