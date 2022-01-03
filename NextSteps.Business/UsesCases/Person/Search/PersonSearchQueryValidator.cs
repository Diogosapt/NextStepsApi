using FluentValidation;

namespace NextSteps.Business.UsesCases
{
    public class PersonSearchQueryValidator : AbstractValidator<PersonSearchQuery>
    {
        public PersonSearchQueryValidator()
        {
            RuleFor(p => p.page)
                .GreaterThan(0)
                .WithMessage("The page number must be greater than zero")
                .WithSeverity(Severity.Error)
                .WithErrorCode("1");

            RuleFor(p => p.page)
                .LessThan(int.MaxValue)
                .WithMessage(string.Format("The page number must be lesser than {0}", int.MaxValue))
                .WithSeverity(Severity.Error)
                .WithErrorCode("2");

            RuleFor(p => p.pageSize)
                .GreaterThan(0)
                .WithMessage("The page size must be greater than zero")
                .WithSeverity(Severity.Error)
                .WithErrorCode("3");

            RuleFor(p => p.pageSize)
                .LessThanOrEqualTo(25)
                .WithMessage("The page size must be less than or equal to 25")
                .WithSeverity(Severity.Error)
                .WithErrorCode("4");

            RuleFor(p => p.filters)
                 .NotNull()
                 .WithMessage("The filters must not be null")
                 .WithSeverity(Severity.Error)
                 .WithErrorCode("5");
        }
    }
}