using Application.Common.Options;
using FluentValidation;
using Microsoft.Extensions.Options;

namespace Application.Projects.Commands.UpdateAttraction;

public class UpdateAttractionCommandValidator : AbstractValidator<UpdateAttractionCommand>
{
    public UpdateAttractionCommandValidator(IOptions<MediaValidationOptions> options)
    {
        var settings = options.Value;
        var maxSvgSizeBytes = settings.MaxSvgSizeMb * 1024 * 1024;

        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("Invalid attraction id");

        RuleFor(x => x.ProjectId)
            .GreaterThan(0).WithMessage("Invalid project id");

        When(x => x.Name != null, () =>
        {
            RuleFor(x => x.Name!.English)
                .NotEmpty().WithMessage("English name is required");
        });

        When(x => x.TimeInMinutes != null, () =>
        {
            RuleFor(x => x.TimeInMinutes!.Value)
                .GreaterThan(0).WithMessage("Time must be greater than 0");
        });

        When(x => x.Icon != null, () =>
        {
            RuleFor(x => x.Icon!)
                .Must(f => f.Length > 0).WithMessage("Icon is empty")
                .Must(f => f.Length <= maxSvgSizeBytes)
                .WithMessage($"Icon must not exceed {settings.MaxSvgSizeMb}MB")
                .Must(f => Path.GetExtension(f.FileName).ToLower() == ".svg")
                .WithMessage("Icon must be an SVG file");
        });
    }
}