using FluentValidation;

namespace NextSteps.Business.UsesCases
{
    public class PersonGetByIdQueryValidator : AbstractValidator<PersonGetByIdQuery>
    {
        public PersonGetByIdQueryValidator()
        {
            RuleFor(p => p.Id)
            .NotEmpty()
            .WithMessage("The ID must be defined")
            .WithSeverity(Severity.Error)
            .WithErrorCode("1");
        }
    }
}