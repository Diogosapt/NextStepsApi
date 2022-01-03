using FluentValidation;
using System;

namespace NextSteps.Business.UsesCases
{
    public class PersonCreateCommandValidator : AbstractValidator<PersonCreateCommand>
    {
        public PersonCreateCommandValidator()
        {
            RuleFor(p => p.Person.Name)
              .NotEmpty()
              .WithMessage("The name must be defined")
              .WithSeverity(Severity.Error)
              .WithErrorCode("1");

            RuleFor(p => p.Person.Surname)
             .NotEmpty()
             .WithMessage("The surname must be defined")
             .WithSeverity(Severity.Error)
             .WithErrorCode("2");

            RuleFor(p => p.Person.Job)
             .NotEmpty()
             .WithMessage("The job must be defined")
             .WithSeverity(Severity.Error)
             .WithErrorCode("3");

            RuleFor(p => p.Person.Email)
             .NotEmpty()
             .WithMessage("The email must be defined")
             .WithSeverity(Severity.Error)
             .WithErrorCode("4");

            RuleFor(p => p.Person.Email)
             .EmailAddress()
             .WithMessage("The email is not a valid e-mail address")
             .WithSeverity(Severity.Error)
             .WithErrorCode("5");

            RuleFor(p => p.Person.Birthday)
             .NotEmpty()
             .WithMessage("The birthday must be defined")
             .WithSeverity(Severity.Error)
             .WithErrorCode("5");

            RuleFor(p => p.Person.Birthday)
             .LessThanOrEqualTo(DateTime.Now)
             .WithMessage("the birthday cannot be that greater than today's date")
             .WithSeverity(Severity.Error)
             .WithErrorCode("6");

            RuleForEach(p => p.Person.Hobbies)
                .SetValidator(new HobbiesValidator())
                .WithMessage("Invalid Hobbies")
                .WithSeverity(Severity.Error)
                .WithErrorCode("7");
        }
    }
}