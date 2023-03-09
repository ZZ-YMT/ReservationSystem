using Application.DTOs.Account;
using FluentValidation.TestHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Application.Tests
{
    public class AuthenticationValidation
    {
        private readonly AuthenticationRequestValidator _validator;

        public AuthenticationValidation()
        {
            _validator = new AuthenticationRequestValidator();
        }

        [Fact]
        public void Should_Not_have_error_when_Email_have_Value()
        {
            var model = new AuthenticationRequest { Email = "kowalski@gmail.com" };

            _validator.TestValidate(model).ShouldNotHaveValidationErrorFor(x => x.Email);
        }

        [Fact]
        public void Should_have_error_when_Email_is_Empty()
        {
            var model = new AuthenticationRequest { Email = string.Empty };

            _validator.TestValidate(model).ShouldHaveValidationErrorFor(x => x.Email);
        }

        [Fact]
        public void Should_have_error_when_Password_is_Empty()
        {
            var model = new AuthenticationRequest { Password = string.Empty };

            _validator.TestValidate(model).ShouldHaveValidationErrorFor(x => x.Password);
        }

        [Fact]
        public void Should_Not_have_error_when_Password_have_Value()
        {
            var model = new AuthenticationRequest { Password = "&Gve&HCD" };

            _validator.TestValidate(model).ShouldNotHaveValidationErrorFor(x => x.Password);
        }
    }
}
