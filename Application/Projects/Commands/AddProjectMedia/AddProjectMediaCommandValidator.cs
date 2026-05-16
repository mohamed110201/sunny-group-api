using Application.Common.Options;
using Domain.Enums;
using FluentValidation;
using Microsoft.Extensions.Options;

namespace Application.Projects.Commands.AddProjectMedia;

public class AddProjectMediaCommandValidator : AbstractValidator<AddProjectMediaCommand>
{
    public AddProjectMediaCommandValidator(IOptions<MediaValidationOptions> options)
    {
        var settings = options.Value;
        var maxImageBytes = settings.MaxImageSizeMb * 1024 * 1024;
        var maxSvgBytes = settings.MaxSvgSizeMb * 1024 * 1024;

        RuleFor(x => x.ProjectId)
            .GreaterThan(0).WithMessage("Invalid project id");

        RuleFor(x => x.Images)
            .NotEmpty().WithMessage("At least one image is required")
            .Must(images => images.Count <= settings.MaxImagesPerUpload)
            .WithMessage($"Maximum {settings.MaxImagesPerUpload} images allowed")
            .Must(images => images.Count(i => i.Category == MediaCategory.Icon) <= 1)
            .WithMessage("Only one icon is allowed per request");

        RuleForEach(x => x.Images)
            .ChildRules(item =>
            {
                item.RuleFor(x => x.Category)
                    .IsInEnum().WithMessage("Invalid media category");

                item.When(x => x.Category == MediaCategory.Icon, () =>
                {
                    item.RuleFor(x => x.Image)
                        .NotNull().WithMessage("Icon is required")
                        .Must(f => f.Length > 0).WithMessage("Icon is empty")
                        .Must(f => f.Length <= maxSvgBytes)
                        .WithMessage($"Icon must not exceed {settings.MaxSvgSizeMb}MB")
                        .Must(f => Path.GetExtension(f.FileName).ToLower() == ".svg")
                        .WithMessage("Icon must be an SVG file");
                });

                item.When(x => x.Category != MediaCategory.Icon, () =>
                {
                    item.RuleFor(x => x.Image)
                        .NotNull().WithMessage("Image is required")
                        .Must(f => f.Length > 0).WithMessage("Image is empty")
                        .Must(f => f.Length <= maxImageBytes)
                        .WithMessage($"Image must not exceed {settings.MaxImageSizeMb}MB")
                        .Must(f => settings.AllowedImageTypes.Contains(f.ContentType.ToLower()))
                        .WithMessage($"Allowed types: {string.Join(", ", settings.AllowedImageTypes)}");
                });
            });
    }
}