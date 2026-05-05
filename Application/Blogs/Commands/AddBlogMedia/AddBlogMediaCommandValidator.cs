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
        var maxSizeBytes = settings.MaxImageSizeMb * 1024 * 1024;

        RuleFor(x => x.BlogId).GreaterThan(0).WithMessage("Invalid blog id");

        RuleFor(x => x.Images)
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