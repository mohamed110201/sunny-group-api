using Application.Common.Options;
using FluentValidation;
using Microsoft.Extensions.Options;

namespace Application.Home.Commands.UpdateVoiceOfCustomer;

public class UpdateVoiceOfCustomerCommandValidator : AbstractValidator<UpdateVoiceOfCustomerCommand>
{
    public UpdateVoiceOfCustomerCommandValidator(IOptions<MediaValidationOptions> options)
    {
        var settings = options.Value;
        var maxSizeBytes = settings.MaxImageSizeMb * 1024 * 1024;
        RuleFor(x => x.Id).GreaterThan(0).WithMessage("Invalid id");
        RuleFor(x => x.CustomerName)
            .NotEmpty().When(x => x.CustomerName != null).WithMessage("Customer name is required")
            .MaximumLength(100).When(x => x.CustomerName != null).WithMessage("Name must not exceed 100 characters");
        RuleFor(x => x.ProjectId).GreaterThan(0).When(x => x.ProjectId.HasValue).WithMessage("Invalid project id");
        RuleFor(x => x.Comment.English).NotEmpty().When(x => x.Comment != null).WithMessage("English comment is required");
        When(x => x.Image != null, () =>
        {
            RuleFor(x => x.Image!)
                .Must(f => f.Length > 0).WithMessage("Image is empty")
                .Must(f => f.Length <= maxSizeBytes)
                .WithMessage($"Image must not exceed {settings.MaxImageSizeMb}MB")
                .Must(f => settings.AllowedImageTypes.Contains(f.ContentType.ToLower()))
                .WithMessage("Image must be jpg, png or webp");
        });
    }
}