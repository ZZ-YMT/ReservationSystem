using System.ComponentModel.DataAnnotations;
using System.Net;

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
                await context.Response.WriteAsync("Brak klucza API.");
                return;
            }

            var configuration = context.RequestServices.GetRequiredService<IConfiguration>();

            var apiKey = configuration.GetValue<string>(_apiKey);

            if (!apiKey.Equals(extractedApiKey))
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Błędny klucz API.");
                return;
            }
        }
    }
}
