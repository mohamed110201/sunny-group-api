using Application.Common.Options;
using FluentValidation;
using Microsoft.Extensions.Options;

namespace Application.Projects.Commands.AddAttraction;

public class AddAttractionCommandValidator : AbstractValidator<AddAttractionCommand>
{
    public AddAttractionCommandValidator(IOptions<MediaValidationOptions> options)
    {
        var settings = options.Value;
        var maxSvgSizeBytes = settings.MaxSvgSizeMb * 1024 * 1024;

        RuleFor(x => x.ProjectId)
            .GreaterThan(0).WithMessage("Invalid project id");

        RuleFor(x => x.Attractions)
            .NotEmpty().WithMessage("At least one attraction is required");

        RuleForEach(x => x.Attractions)
            .ChildRules(item =>
            {
                item.RuleFor(x => x.Name.English)
                    .NotEmpty().WithMessage("English name is required");

                item.RuleFor(x => x.TimeInMinutes)
                    .GreaterThan(0).WithMessage("Time must be greater than 0");

                item.RuleFor(x => x.Icon)
                    .NotNull().WithMessage("Icon is required")
                    .Must(f => f.Length > 0).WithMessage("Icon is empty")
                    .Must(f => f.Length <= maxSvgSizeBytes)
                    .WithMessage($"Icon must not exceed {settings.MaxSvgSizeMb}MB")
                    .Must(f => Path.GetExtension(f.FileName).ToLower() == ".svg")
                    .WithMessage("Icon must be an SVG file");
            });
    }
}