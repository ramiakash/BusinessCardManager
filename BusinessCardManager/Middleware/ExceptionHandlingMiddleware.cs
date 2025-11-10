using BusinessCardManager.Application.Common.Exceptions;
using BusinessCardManager.Domain.Exceptions;
using FluentValidation;
using System.Text.Json;

namespace BusinessCardManager.API.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            var statusCode = StatusCodes.Status500InternalServerError;
            object problem = new
            {
                Title = "An unexpected error occurred.",
                Detail = exception.Message
            };

            switch (exception)
            {
                case ValidationException validationException:
                    statusCode = StatusCodes.Status400BadRequest;
                    problem = new { Title = "Validation Error", Errors = validationException.Errors.Select(e => new { e.PropertyName, e.ErrorMessage }) };
                    break;

                case NotFoundException notFoundException:
                    statusCode = StatusCodes.Status404NotFound;
                    problem = new { Title = "Not Found", Detail = notFoundException.Message };
                    break;

                case InvalidEmailException or InvalidPhoneException:
                    statusCode = StatusCodes.Status400BadRequest;
                    problem = new { Title = "Invalid Input", Detail = exception.Message };
                    break;
            }

            context.Response.StatusCode = statusCode;
            return context.Response.WriteAsync(JsonSerializer.Serialize(problem));
        }
    }
}
