using FluentValidation;

namespace NextSteps.Business.UsesCases
{
    public class PersonDeleteCommandValidator : AbstractValidator<PersonDeleteCommand>
    {
        public PersonDeleteCommandValidator()
        {
            RuleFor(p => p.Id)
            .NotEmpty()
            .WithMessage("The ID must be defined")
            .WithSeverity(Severity.Error)
            .WithErrorCode("1");
        }
    }
}