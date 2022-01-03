using FluentValidation;
using FluentValidation.TestHelper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NextSteps.Business.UsesCases;
using System;

namespace NextSteps.Business.Tests.UseCases.Person.Delete
{
    [TestClass]
    public class PersonDeleteCommandValidatorTests
    {
        private static PersonDeleteCommandValidator validator;

        [ClassInitialize]
        public static void Setup(TestContext context)
        {
            validator = new PersonDeleteCommandValidator();
        }

        [TestMethod]
        public void Should_not_have_error_when_Id_is_valid()
        {
            // Arrange
            var id = Guid.Parse("1ae1f7b5-3a33-4137-8091-c4b079a48012");
            var query = new PersonDeleteCommand(id);

            // Act
            var result = validator.TestValidate(query);

            // Assert
            result.ShouldNotHaveValidationErrorFor(q => q.Id);
        }

        [TestMethod]
        public void Should_have_error_when_Id_is_empty()
        {
            // Arrange
            var id = Guid.Empty;
            var query = new PersonDeleteCommand(id);

            // Act
            var result = validator.TestValidate(query);

            // Assert
            result.ShouldHaveValidationErrorFor(q => q.Id)
              .WithErrorMessage("The Person Id must be defined")
              .WithSeverity(Severity.Error)
              .WithErrorCode("1");
        }
    }
}