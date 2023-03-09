
using API.Extensions;
using API.Middlewares;
using Application;
using Domain.Exceptions;
using Infrastructure;
using Infrastructure.Identity.Contexts;
using Infrastructure.Identity.Models;
using Infrastructure.Identity.Seeds;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Persistence;

namespace API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args); 

            builder.Services.AddDbContext<IdentityContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));

            });

            builder.Services.AddApplication();
            builder.Services.AddInfrastructure(builder.Configuration);
            builder.Services.AddPersistence();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerExtension();

            builder.Services.AddControllers();    

            builder.Services.AddCors(opt =>
            {
                opt.AddPolicy("CorsPolicy", policy =>
                {
                    //todo change for production.
                    policy.AllowAnyMethod().AllowAnyHeader().WithOrigins("*");
                });
            });

            var app = builder.Build();
            

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwaggerExtension();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseMiddleware<ApiKeyMiddleware>();

            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseCors("CorsPolicy");            

            app.MapControllers();

            app.Run();
        }
    }
}