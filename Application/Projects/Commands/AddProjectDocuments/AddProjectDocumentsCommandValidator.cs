using Application.Common.Options;
using Application.Common.Validators;
using FluentValidation;
using Microsoft.Extensions.Options;

namespace Application.Projects.Commands.AddProjectDocuments;

public class AddProjectDocumentsCommandValidator : AbstractValidator<AddProjectDocumentsCommand>
{
    public AddProjectDocumentsCommandValidator(IOptions<DocumentValidationOptions> options)
    {
        var settings = options.Value;

        RuleFor(x => x.ProjectId)
            .GreaterThan(0).WithMessage("Invalid project id");

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