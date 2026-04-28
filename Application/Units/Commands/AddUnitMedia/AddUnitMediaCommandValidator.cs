using Application.Common.Options;
using Domain.Enums;
using FluentValidation;
using Microsoft.Extensions.Options;

namespace Application.Units.Commands.AddUnitMedia;

public class AddUnitMediaCommandValidator : AbstractValidator<AddUnitMediaCommand>
{
    public AddUnitMediaCommandValidator(IOptions<MediaValidationOptions> options)
    {
        var settings = options.Value;
        var maxSizeBytes = settings.MaxImageSizeMb * 1024 * 1024;

        RuleFor(x => x.UnitId).GreaterThan(0).WithMessage("Invalid unit id");
        RuleFor(x => x.ProjectId).GreaterThan(0).WithMessage("Invalid project id");

        RuleFor(x => x.Images)
            .Must(images => images.Count <= settings.MaxImagesPerUpload)
            .WithMessage($"Maximum {settings.MaxImagesPerUpload} images allowed");

        RuleForEach(x => x.Images)
            .ChildRules(item =>
            {
                item.RuleFor(x => x.Category)
                    .IsInEnum().WithMessage("Invalid media category");

                item.When(x => x.Category == MediaCategory.Gallery || x.Category == MediaCategory.Inventory, () =>
                {
                    item.RuleFor(x => x.SubCategory)
                        .NotNull().WithMessage("SubCategory is required for gallery and inventory images");
                });

                item.When(x => x.Category == MediaCategory.Gallery, () =>
                {
                    item.RuleFor(x => x.SubCategory)
                        .Must(s => s == MediaSubCategory.Interior || s == MediaSubCategory.Exterior)
                        .WithMessage("Gallery subcategory must be Interior or Exterior");
                });

                item.When(x => x.Category == MediaCategory.Inventory, () =>
                {
                    item.RuleFor(x => x.SubCategory)
                        .Must(s => s == MediaSubCategory.BlackAndWhite || s == MediaSubCategory.Colored)
                        .WithMessage("Inventory subcategory must be BlackAndWhite or Colored");
                });

                item.RuleFor(x => x.Image)
                    .NotNull().WithMessage("Image is required")
                    .Must(f => f.Length > 0).WithMessage("Image is empty")
                    .Must(f => f.Length <= maxSizeBytes)
                    .WithMessage($"Image must not exceed {settings.MaxImageSizeMb}MB")
                    .Must(f => settings.AllowedImageTypes.Contains(f.ContentType.ToLower()))
                    .WithMessage($"Allowed types: {string.Join(", ", settings.AllowedImageTypes)}");
            });
    }
}