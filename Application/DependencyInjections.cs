using Application.DTOs.Account;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public static class DependencyInjections
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {            
            services.AddFluentValidationAutoValidation();
            
            services.AddScoped<IValidator<RegisterRequest>, RegisterRequestValidator>();
            services.AddScoped<IValidator<AuthenticationRequest>, AuthenticationRequestValidator>();

            return services;
        }
    }
}
