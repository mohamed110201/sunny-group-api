using Application.Common.Options;
using FluentValidation;
using Microsoft.Extensions.Options;

namespace Application.Home.Commands.UpdateHomeMedia;

public class UpdateHomeMediaCommandValidator : AbstractValidator<UpdateHomeMediaCommand>
{
    public UpdateHomeMediaCommandValidator(IOptions<MediaValidationOptions> options)
    {
        var settings = options.Value;
        var maxImageBytes = settings.MaxImageSizeMb * 1024 * 1024;
        var maxVideoBytes = settings.MaxVideoSizeMb * 1024 * 1024;
        var allowedTypes = settings.AllowedImageTypes
            .Concat(settings.AllowedVideoTypes)
            .ToArray();

        RuleFor(x => x.Media)
            .NotNull().WithMessage("Media is required")
            .Must(f => f.Length > 0).WithMessage("Media file is empty")
            .Must(f => allowedTypes.Contains(f.ContentType.ToLower()))
            .WithMessage($"Allowed types: {string.Join(", ", allowedTypes)}")
            .Must(f =>
            {
                if (settings.AllowedImageTypes.Contains(f.ContentType.ToLower()))
                    return f.Length <= maxImageBytes;
                if (settings.AllowedVideoTypes.Contains(f.ContentType.ToLower()))
                    return f.Length <= maxVideoBytes;
                return false;
            })
            .WithMessage($"Image must not exceed {settings.MaxImageSizeMb}MB, video must not exceed {settings.MaxVideoSizeMb}MB");
    }
}