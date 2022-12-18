using DoctorAppointment.Api.Exceptions;
using EstateWebManager.API.Exceptions;
using Newtonsoft.Json;
using System.Net;

namespace DoctorAppointment.Api.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, message: exception.Message);
                await HandleGlobalExceptionAsync(httpContext, exception);
            }
        }

        private static async Task HandleGlobalExceptionAsync(HttpContext context, Exception exception)
        {
            var code = HttpStatusCode.InternalServerError; // 500 if unexpected

            if (exception is NotFoundException) code = HttpStatusCode.NotFound;
            else if (exception is UnauthorizedException) code = HttpStatusCode.Unauthorized;
            else if (exception is ForbiddenException) code = HttpStatusCode.Forbidden;
            else if (exception is BadRequestException) code = HttpStatusCode.BadRequest;

            var result = JsonConvert.SerializeObject(new { error = exception.Message });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            await context.Response.WriteAsync(result);
            if (exception.InnerException != null) await context.Response.WriteAsync(exception.InnerException.Message);
            if (exception.StackTrace != null) await context.Response.WriteAsync(exception.StackTrace);
        }
    }

    public static class GlobalExceptionMiddleware
    {
        public static IApplicationBuilder UseGlobalExceptionMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
