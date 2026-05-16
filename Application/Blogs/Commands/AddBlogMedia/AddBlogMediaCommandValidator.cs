using Application.Common.Options;
using Domain.Enums;
using FluentValidation;
using Microsoft.Extensions.Options;

namespace Application.Blogs.Commands.AddBlogMedia;

public class AddBlogMediaCommandValidator : AbstractValidator<AddBlogMediaCommand>
{
    public AddBlogMediaCommandValidator(IOptions<MediaValidationOptions> options)
    {
        var settings = options.Value;
        var maxImageBytes = settings.MaxImageSizeMb * 1024 * 1024;
        var maxVideoBytes = settings.MaxVideoSizeMb * 1024 * 1024;
        var allowedTypes = settings.AllowedImageTypes
            .Concat(settings.AllowedVideoTypes)
            .ToArray();

        RuleFor(x => x.BlogId)
            .GreaterThan(0).WithMessage("Invalid blog id");

        // validate the list itself
        RuleFor(x => x.Media)
            .NotEmpty().WithMessage("At least one file is required")
            .Must(media => media.Count <= settings.MaxImagesPerUpload)
            .WithMessage($"Maximum {settings.MaxImagesPerUpload} files allowed");

        // validate each item in the list
        RuleForEach(x => x.Media)
            .ChildRules(item =>
            {
                item.RuleFor(x => x.Category)
                    .IsInEnum().WithMessage("Invalid media category");

                item.RuleFor(x => x.File)
                    .NotNull().WithMessage("File is required")
                    .Must(f => f.Length > 0).WithMessage("File is empty")
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
            });
    }
}