using FluentValidation;

namespace Application.Team.Commands.UpdateTeamMember;

public class UpdateTeamMemberCommandValidator : AbstractValidator<UpdateTeamMemberCommand>
{
    public UpdateTeamMemberCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("Invalid team member id");

        When(x => x.Name != null, () =>
        {
            RuleFor(x => x.Name)
                .ChildRules(name =>
                {
                    name.RuleFor(n => n!.English)
                        .NotEmpty().WithMessage("English name is required");
                });
        });

        When(x => x.Position != null, () =>
        {
            RuleFor(x => x.Position)
                .ChildRules(position =>
                {
                    position.RuleFor(p => p!.English)
                        .NotEmpty().WithMessage("English position is required");
                });
        });

        When(x => x.Department != null, () =>
        {
            RuleFor(x => x.Department)
                .ChildRules(department =>
                {
                    department.RuleFor(d => d!.English)
                        .NotEmpty().WithMessage("English department is required");
                });
        });

        When(x => !string.IsNullOrWhiteSpace(x.Email), () =>
        {
            RuleFor(x => x.Email)
                .EmailAddress().WithMessage("Invalid email address");
        });
    }
}
