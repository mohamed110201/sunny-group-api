using Application.Common.Options;
using FluentValidation;
using Microsoft.Extensions.Options;

namespace Application.Home.Commands.UpdateHomeMedia;

public class UpdateHomeMediaCommandValidator : AbstractValidator<UpdateHomeMediaCommand>
{
    public UpdateHomeMediaCommandValidator(IOptions<MediaValidationOptions> options)
    {
        var settings = options.Value;
        var maxSizeBytes = settings.MaxImageSizeMb * 1024 * 1024;

        RuleFor(x => x.Media)
            .NotNull().WithMessage("Media is required")
            .Must(f => f.Length <= maxSizeBytes)
            .WithMessage($"Media must not exceed {settings.MaxImageSizeMb}MB")
            .Must(f => settings.AllowedImageTypes.Contains(f.ContentType.ToLower())
                       || f.ContentType == "video/mp4")
            .WithMessage("Media must be an image or mp4 video");
    }
}