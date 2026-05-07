using Application.Common.Options;
using Application.Common.Validators;
using FluentValidation;
using Microsoft.Extensions.Options;

namespace Application.Units.Commands.AddUnitDocuments;

public class AddUnitDocumentsCommandValidator : AbstractValidator<AddUnitDocumentsCommand>
{
    public AddUnitDocumentsCommandValidator(IOptions<DocumentValidationOptions> options)
    {
        var settings = options.Value;

        RuleFor(x => x.UnitId).GreaterThan(0).WithMessage("Invalid unit id");
        RuleFor(x => x.ProjectId).GreaterThan(0).WithMessage("Invalid project id");

        RuleFor(x => x.Documents)
            .NotEmpty().WithMessage("At least one document is required")
            .Must(d => d.Count <= settings.MaxDocumentsPerUpload)
            .WithMessage($"Maximum {settings.MaxDocumentsPerUpload} documents allowed");

        RuleForEach(x => x.Documents)
            .ChildRules(item =>
            {
                item.RuleFor(x => x.Category)
                    .IsInEnum().WithMessage("Invalid document category");
                item.RuleFor(x => x.Document)
                    .ValidateDocument(settings);
            });
    }
}