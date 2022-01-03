using FluentValidation;

namespace NextSteps.Business.UsesCases
{
    public class HobbiesValidator : AbstractValidator<Models.Hobbies>
    {
        public HobbiesValidator()
        {
            RuleFor(p => p.Hobby)
                 .NotEmpty()
                 .WithMessage("The hobby must be defined")
                 .WithSeverity(Severity.Error)
                 .WithErrorCode("1");
        }
    }
}