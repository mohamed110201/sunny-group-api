using FluentValidation;

namespace Application.Team.Commands.CreateTeamMember;

public class CreateTeamMemberCommandValidator : AbstractValidator<CreateTeamMemberCommand>
{
    public CreateTeamMemberCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotNull().WithMessage("Name is required")
            .ChildRules(name =>
            {
                name.RuleFor(n => n!.English)
                    .NotEmpty().WithMessage("English name is required");
            });

        RuleFor(x => x.Position)
            .NotNull().WithMessage("Position is required")
            .ChildRules(position =>
            {
                position.RuleFor(p => p!.English)
                    .NotEmpty().WithMessage("English position is required");
            });

        RuleFor(x => x.Department)
            .NotNull().WithMessage("Department is required")
            .ChildRules(department =>
            {
                department.RuleFor(d => d!.English)
                    .NotEmpty().WithMessage("English department is required");
            });

        When(x => !string.IsNullOrWhiteSpace(x.Email), () =>
        {
            RuleFor(x => x.Email)
                .EmailAddress().WithMessage("Invalid email address");
        });
    }
}
