using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Account
{
    public class AuthenticationRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class AuthenticationRequestValidator : AbstractValidator<AuthenticationRequest>
    {
        public AuthenticationRequestValidator()
        {            
            RuleFor(x => x.Email).NotEmpty().EmailAddress();            
            RuleFor(x => x.Password).NotEmpty();          
        }
    }
}
