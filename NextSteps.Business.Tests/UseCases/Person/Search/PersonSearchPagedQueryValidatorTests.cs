using FluentValidation;
using FluentValidation.TestHelper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NextSteps.Business.Models;
using NextSteps.Business.UsesCases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextSteps.Business.Tests
{
    [TestClass]
    public class PersonSearchPagedQueryValidatorTests
    {
        private static PersonSearchQueryValidator validator;

        [ClassInitialize]
        public static void Setup(TestContext context)
        {
            validator = new PersonSearchQueryValidator();
        }

        [TestMethod]
        public void Should_not_have_error_when_All_Parameters_OK()
        {
            // Arrange
            var page = 1;
            var pageSize = 5;
            var filter = new Filters
            {
                Name = "Antonio",
                Surname = "Silva",
                Email = "antonio@email.pt",
                Job = "Programador"
            };

            var query = new PersonSearchQuery(filter, page, pageSize);

            // Act
            var result = validator.TestValidate(query);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }

        [TestMethod]
        public void Should_have_error_when_Page_is_not_greater_than_zero()
        {
            // Arrange
            var page = 0;
            var pageSize = 5;
            var filter = new Filters
            {
                Name = "Antonio",
                Surname = "Silva",
                Email = "antonio@email.pt",
                Job = "Programador"
            };

            var query = new PersonSearchQuery(filter, page, pageSize);

            // Act
            var result = validator.TestValidate(query);

            // Assert
            result.ShouldHaveValidationErrorFor(q => q.page)
              .WithErrorMessage("The page number must be greater than zero")
              .WithSeverity(Severity.Error)
              .WithErrorCode("1");
        }

        [TestMethod]
        public void Should_have_error_when_Page_is_not_lesser_than_MaxValue()
        {
            // Arrange
            var page = int.MaxValue;
            var pageSize = 5;
            var filter = new Filters
            {
                Name = "Antonio",
                Surname = "Silva",
                Email = "antonio@email.pt",
                Job = "Programador"
            };

            var query = new PersonSearchQuery(filter, page, pageSize);

            // Act
            var result = validator.TestValidate(query);

            // Assert
            result.ShouldHaveValidationErrorFor(q => q.page)
              .WithErrorMessage(string.Format("The page number must be lesser than {0}", int.MaxValue))
              .WithSeverity(Severity.Error)
              .WithErrorCode("2");
        }

        [TestMethod]
        public void Should_have_error_when_PageSize_is_not_greater_than_zero()
        {
            // Arrange
            var page = 1;
            var pageSize = 0;
            var filter = new Filters
            {
                Name = "Antonio",
                Surname = "Silva",
                Email = "antonio@email.pt",
                Job = "Programador"
            };

            var query = new PersonSearchQuery(filter, page, pageSize);

            // Act
            var result = validator.TestValidate(query);

            // Assert
            result.ShouldHaveValidationErrorFor(q => q.pageSize)
              .WithErrorMessage("The page size must be greater than zero")
              .WithSeverity(Severity.Error)
              .WithErrorCode("3");
        }

        [TestMethod]
        public void Should_have_error_when_PageSize_is_not_lesserOrEqual_than_25()
        {
            // Arrange
            var page = 1;
            var pageSize = 26;
            var filter = new Filters
            {
                Name = "Antonio",
                Surname = "Silva",
                Email = "antonio@email.pt",
                Job = "Programador"
            };

            var query = new PersonSearchQuery(filter, page, pageSize);

            // Act
            var result = validator.TestValidate(query);

            // Assert
            result.ShouldHaveValidationErrorFor(q => q.pageSize)
              .WithErrorMessage("The page size must be less than or equal to 25")
              .WithSeverity(Severity.Error)
              .WithErrorCode("4");
        }

        [TestMethod]
        public void Should_have_error_when_Email_is_invalid()
        {
            // Arrange
            var page = 1;
            var pageSize = 5;
            var filter = new Filters
            {
                Name = "Antonio",
                Surname = "Silva",
                Email = "email-error",
                Job = "Programador"
            };

            var query = new PersonSearchQuery(filter, page, pageSize);

            // Act
            var result = validator.TestValidate(query);

            // Assert
            result.ShouldHaveValidationErrorFor(q => q.filters.Email)
              .WithErrorMessage("The email is not a valid e-mail address")
              .WithSeverity(Severity.Error)
              .WithErrorCode("5");
        }
    }
}