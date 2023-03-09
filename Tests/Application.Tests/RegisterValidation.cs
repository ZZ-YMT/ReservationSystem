using Application.DTOs.Account;
using FluentValidation.TestHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Infrastructure.Tests.Identity
{
    public class RegisterValidation
    {
        private readonly RegisterRequestValidator _validator;

        public RegisterValidation()
        {
            _validator = new RegisterRequestValidator();
        }

        [Fact]
        public void Should_have_error_when_FirstName_is_Empty()
        {
            var model = new RegisterRequest { FirstName = string.Empty };

            _validator.TestValidate(model).ShouldHaveValidationErrorFor(x => x.FirstName);
        }

        [Fact]
        public void Should_Not_have_error_when_FirstName_have_Value()
        {
            var model = new RegisterRequest { FirstName = "Piotr" };

            _validator.TestValidate(model).ShouldNotHaveValidationErrorFor(x => x.FirstName);
        }

        [Fact]
        public void Should_have_error_when_LastName_is_Empty()
        {
            var model = new RegisterRequest { LastName = string.Empty };

            _validator.TestValidate(model).ShouldHaveValidationErrorFor(x => x.LastName);
        }

        [Fact]
        public void Should_Not_have_error_when_LastName_have_Value()
        {
            var model = new RegisterRequest { LastName = "Kowalski" };

            _validator.TestValidate(model).ShouldNotHaveValidationErrorFor(x => x.LastName);
        }

        [Fact]
        public void Should_have_error_when_Email_is_Empty()
        {
            var model = new RegisterRequest { Email = string.Empty };

            _validator.TestValidate(model).ShouldHaveValidationErrorFor(x => x.Email);
        }

        [Fact]
        public void Should_Not_have_error_when_Email_have_Value()
        {
            var model = new RegisterRequest { Email = "kowalski@wp.pl" };

            _validator.TestValidate(model).ShouldNotHaveValidationErrorFor(x => x.Email);
        }

        [Fact]
        public void Should_have_error_when_UserName_is_Empty()
        {
            var model = new RegisterRequest { UserName = string.Empty };

            _validator.TestValidate(model).ShouldHaveValidationErrorFor(x => x.UserName);
        }

        [Fact]
        public void Should_have_error_when_UserName_Leng_is_Short()
        {
            var model = new RegisterRequest { UserName = "1234" };

            _validator.TestValidate(model).ShouldHaveValidationErrorFor(x => x.UserName);
        }

        [Fact]
        public void Should_Not_have_error_when_UserName_Leng_is_More_then_5()
        {
            var model = new RegisterRequest { UserName = "123456" };

            _validator.TestValidate(model).ShouldNotHaveValidationErrorFor(x => x.UserName);
        }

        [Fact]
        public void Should_have_error_when_Password_is_Empty()
        {
            var model = new RegisterRequest { Password = string.Empty };

            _validator.TestValidate(model).ShouldHaveValidationErrorFor(x => x.Password);
        }

        [Fact]
        public void Should_have_error_when_Password_Leng_is_Short()
        {
            var model = new RegisterRequest { Password = "7t!A", ConfirmPassword = "7t!A" };

            _validator.TestValidate(model).ShouldHaveValidationErrorFor(x => x.Password);
        }

        [Fact]
        public void Should_Not_have_error_when_Password_Leng_is_More_then_5()
        {
            var model = new RegisterRequest { Password = "7t!Azv", ConfirmPassword = "7t!Azv" };

            _validator.TestValidate(model).ShouldNotHaveValidationErrorFor(x => x.Password);
        }

        [Fact]
        public void Should_have_error_when_Password_Not_Compare_ConfirmPassword()
        {
            var model = new RegisterRequest { Password = "7t!Azv", ConfirmPassword = "7t!Az" };

            _validator.TestValidate(model).ShouldHaveValidationErrorFor(x => x.ConfirmPassword);
        }
    }
}
