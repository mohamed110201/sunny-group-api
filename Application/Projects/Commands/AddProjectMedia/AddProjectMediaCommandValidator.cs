using Application.Common.Options;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Application.Projects.Commands.AddProjectMedia;

public class AddProjectMediaCommandValidator : AbstractValidator<AddProjectMediaCommand>
{
    public AddProjectMediaCommandValidator(IOptions<MediaValidationOptions> options)
    {
        var settings = options.Value;
        var maxSizeBytes = settings.MaxImageSizeMb * 1024 * 1024;

        RuleFor(x => x.ProjectId)
            .GreaterThan(0).WithMessage("Invalid project id");

        RuleFor(x => x.Images)
            .NotEmpty().WithMessage("At least one image is required")
            .Must(images => images.Count <= settings.MaxImagesPerUpload)
            .WithMessage($"Maximum {settings.MaxImagesPerUpload} images allowed");

        RuleForEach(x => x.Images)
            .ChildRules(item =>
            {
                item.RuleFor(x => x.Category)
                    .IsInEnum().WithMessage("Invalid media category");

                item.RuleFor(x => x.Image)
                    .Must(f => f.Length <= maxSizeBytes)
                    .WithMessage($"Image must not exceed {settings.MaxImageSizeMb}MB")
                    .Must(f => settings.AllowedImageTypes.Contains(f.ContentType.ToLower()))
                    .WithMessage($"Allowed types: {string.Join(", ", settings.AllowedImageTypes)}");
            });
    }
}
