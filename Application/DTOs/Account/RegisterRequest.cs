using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Account
{
    public class RegisterRequest
    {      
        public string FirstName { get; set; }        
        public string LastName { get; set; }
        public string Email { get; set; }       
        public string UserName { get; set; }       
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }

    public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
    {
        public RegisterRequestValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty();
            RuleFor(x => x.LastName).NotEmpty();
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.UserName).NotEmpty().MinimumLength(6);           
            RuleFor(m => m.Password).NotEmpty().MinimumLength(6);
            RuleFor(x => x.ConfirmPassword).NotEmpty().Equal(m => m.Password);
        }
    }
}
