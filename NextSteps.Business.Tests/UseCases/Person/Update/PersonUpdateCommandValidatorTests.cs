using FluentValidation;
using FluentValidation.TestHelper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NextSteps.Business.Tests.Builder;
using NextSteps.Business.UsesCases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextSteps.Business.Tests.UseCases.Person.Update
{
    [TestClass]
    public class PersonUpdateCommandValidatorTests
    {
        private static PersonUpdateCommandValidator validator;

        private readonly string name = "Antonio";
        private readonly string surname = "Silva";
        private readonly string job = "Programador";
        private readonly DateTime birthday = new DateTime(2000, 03, 10);
        private readonly string email = "antonio@email.pt";

        [ClassInitialize]
        public static void Setup(TestContext context)
        {
            validator = new PersonUpdateCommandValidator();
        }

        [TestMethod]
        public void Should_not_have_error_when_All_Parameters_Ok()
        {
            var person = new PersonBuilder().WithTestValues().Build();

            var command = new PersonUpdateCommand(person);

            var result = validator.TestValidate(command);

            result.ShouldHaveAnyValidationError();
        }

        #region id

        public void Should_have_error_when_person_Id_is_empty()
        {
            //Arrange
            var person = new PersonBuilder()
                .WithTestValues()
                .With_Id(Guid.Empty)
                .Build();

            var command = new PersonUpdateCommand(person);
            // Act
            var result = validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(p => p.Person.Id)
                  .WithErrorMessage("The product Id must be defined")
                  .WithSeverity(Severity.Error)
                  .WithErrorCode("1");
        }

        #endregion id

        #region email

        [TestMethod]
        public void Should_have_error_when_Email_is_null()
        {
            //Arrange
            var person = new PersonBuilder()
                .WithTestValues()
                .With_Email(null)
                .Build();

            var command = new PersonUpdateCommand(person);

            // Act
            var result = validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(p => p.Person.Email)
                  .WithErrorMessage("The email must be defined")
                  .WithSeverity(Severity.Error)
                  .WithErrorCode("2");
        }

        [TestMethod]
        public void Should_have_error_when_Email_is_empty()
        {
            //Arrange
            var person = new PersonBuilder()
                .WithTestValues()
                .With_Email(" ")
                .Build();

            var command = new PersonUpdateCommand(person);

            // Act
            var result = validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(p => p.Person.Email)
                  .WithErrorMessage("The email must be defined")
                  .WithSeverity(Severity.Error)
                  .WithErrorCode("2");
        }

        [TestMethod]
        public void Should_have_error_when_Email_is_invalid()
        {
            //Arrange
            var person = new PersonBuilder()
                .WithTestValues()
                .With_Email("invalid email")
                .Build();

            var command = new PersonUpdateCommand(person);

            // Act
            var result = validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(p => p.Person.Email)
                  .WithErrorMessage("The email must be defined")
                  .WithSeverity(Severity.Error)
                  .WithErrorCode("2");
        }

        #endregion email

        #region name

        public void Should_have_error_when_Person_Name_is_null()
        {
            //Arrange
            var person = new PersonBuilder()
                .WithTestValues()
                .With_Name(null)
                .Build();

            var command = new PersonUpdateCommand(person);

            // Act
            var result = validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(p => p.Person.Name)
                  .WithErrorMessage("The person Name must be defined")
                  .WithSeverity(Severity.Error)
                  .WithErrorCode("3");
        }

        [TestMethod]
        public void Should_have_error_when_Person_Name_is_empty()
        {
            //Arrange
            var person = new PersonBuilder()
                .WithTestValues()
                .With_Name(" ")
                .Build();

            var command = new PersonUpdateCommand(person);

            // Act
            var result = validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(p => p.Person.Name)
                  .WithErrorMessage("The person Name must be defined")
                  .WithSeverity(Severity.Error)
                  .WithErrorCode("3");
        }

        #endregion name

        #region surname

        public void Should_have_error_when_person_Surname_is_null()
        {
            //Arrange
            var person = new PersonBuilder()
                .WithTestValues()
                .With_Surname(null)
                .Build();

            var command = new PersonUpdateCommand(person);

            // Act
            var result = validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(p => p.Person.Surname)
                  .WithErrorMessage("The person Surname must be defined")
                  .WithSeverity(Severity.Error)
                  .WithErrorCode("4");
        }

        [TestMethod]
        public void Should_have_error_when_person_Surname_is_empty()
        {
            //Arrange
            var person = new PersonBuilder()
                .WithTestValues()
                .With_Name(" ")
                .Build();

            var command = new PersonUpdateCommand(person);

            // Act
            var result = validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(p => p.Person.Surname)
                  .WithErrorMessage("The person Surname must be defined")
                  .WithSeverity(Severity.Error)
                  .WithErrorCode("4");
        }

        #endregion surname

        #region job

        public void Should_have_error_when_Person_Job_is_null()
        {
            //Arrange
            var person = new PersonBuilder()
                .WithTestValues()
                .With_Job(null)
                .Build();

            var command = new PersonUpdateCommand(person);

            // Act
            var result = validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(p => p.Person.Job)
                  .WithErrorMessage("The person Job must be defined")
                  .WithSeverity(Severity.Error)
                  .WithErrorCode("5");
        }

        [TestMethod]
        public void Should_have_error_when_Person_Job_is_empty()
        {
            //Arrange
            var person = new PersonBuilder()
                .WithTestValues()
                .With_Job(" ")
                .Build();

            var command = new PersonUpdateCommand(person);

            // Act
            var result = validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(p => p.Person.Job)
                  .WithErrorMessage("The person Job must be defined")
                  .WithSeverity(Severity.Error)
                  .WithErrorCode("5");
        }

        #endregion job

        #region birthday

        public void Should_have_error_when_Person_Birthday_is_null()
        {
            //Arrange
            var person = new PersonBuilder()
                .WithTestValues()
                .With_Birthday(default)
                .Build();

            var command = new PersonUpdateCommand(person);

            // Act
            var result = validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(p => p.Person.Surname)
                  .WithErrorMessage("The birthday must be defined")
                  .WithSeverity(Severity.Error)
                  .WithErrorCode("5");
        }

        #endregion birthday
    }
}