using Application.Wrappers;
using Azure;
using Domain.Exceptions;
using System.Net;
using System.Text.Json;

namespace API.Middlewares
{
    public class ApiKeyMiddleware
    {
        private readonly RequestDelegate _next;
        private const string _apiKey = "x-api-key";
        public ApiKeyMiddleware(RequestDelegate next) 
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (!context.Request.Headers.TryGetValue(_apiKey, out var extractedApiKey))
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("No API key was provided.");
                return;
            }

            var configuration = context.RequestServices.GetRequiredService<IConfiguration>();

            var apiKey = configuration.GetValue<string>(_apiKey);

            if (!apiKey.Equals(extractedApiKey))
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Invalid API key.");
                return;
            }

            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                var response = context.Response;
                response.ContentType = "application/json";
                var responseModel = new Application.Wrappers.Response<string>() { Succeeded = false, Message = error?.Message };

                switch (error)
                {
                    case ApiException e:

                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;
                    case ValidationException e:

                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        responseModel.Errors = e.Errors;
                        break;
                    case KeyNotFoundException e:

                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        break;
                    default:

                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }
                var result = JsonSerializer.Serialize(responseModel);

                await response.WriteAsync(result);
            }           
        }

    }
}
