using Application.Common.Options;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace Application.Common.Validators;

public static class DocumentValidator
{
    public static IRuleBuilderOptions<T, IFormFile> ValidateDocument<T>(
        this IRuleBuilder<T, IFormFile> ruleBuilder,
        DocumentValidationOptions settings)
    {
        var maxSizeBytes = settings.MaxSizeMb * 1024 * 1024;

        return ruleBuilder
            .NotNull().WithMessage("File is required")
            .Must(f => f.Length > 0).WithMessage("File is empty")
            .Must(f => f.Length <= maxSizeBytes)
            .WithMessage($"File must not exceed {settings.MaxSizeMb}MB")
            .Must(f => settings.AllowedTypes.Contains(f.ContentType.ToLower()))
            .WithMessage($"Allowed types: {string.Join(", ", settings.AllowedTypes)}");
    }
}