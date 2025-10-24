using Microsoft.AspNetCore.Mvc;
using PurchasingSystem.Domain.Shared.Errors;
using PurchasingSystem.Domain.Shared.Exceptions;
using System.Text.Json;

namespace PurchasingSystem.Api.Middleware
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlerMiddleware> _logger;

        public ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (DomainValidationException ex)
            {
                await HandleValidationExceptionAsync(context, ex);
            }
            catch (DomainException ex)
            {
                await HandleDomainExceptionAsync(context, ex);
            }
            catch (Exception ex)
            {
                await HandleGenericExceptionAsync(context, ex);
            }
        }

        private async Task HandleValidationExceptionAsync(HttpContext context, DomainValidationException exception)
        {
            _logger.LogWarning("Ocorreu uma exceção de validação.");

            var errors = exception.Errors.ToDictionary(e => e.Code, e => new[] { e.Message });

            var problemDetails = new ValidationProblemDetails(errors)
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Ocorreram um ou mais erros de validação.",
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1"
            };

            context.Response.ContentType = "application/problem+json";
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            await context.Response.WriteAsync(JsonSerializer.Serialize(problemDetails));
        }

        private async Task HandleDomainExceptionAsync(HttpContext context, DomainException exception)
        {
            _logger.LogWarning("Ocorreu uma exceção de domínio: {ErrorType} - {ErrorMessage}", exception.Error.Type, exception.Error.Message);

            var statusCode = exception.Error.Type switch
            {
                ErrorType.NotFound => StatusCodes.Status404NotFound,
                ErrorType.Conflict => StatusCodes.Status409Conflict,
                ErrorType.Forbidden => StatusCodes.Status403Forbidden,
                _ => StatusCodes.Status400BadRequest
            };

            var problemDetails = new ProblemDetails
            {
                Status = statusCode,
                Title = exception.Error.Message,
                Type = $"urn:purchasingsystem:error:{exception.Error.Code}"
            };

            context.Response.ContentType = "application/problem+json";
            context.Response.StatusCode = statusCode;
            await context.Response.WriteAsync(JsonSerializer.Serialize(problemDetails));
        }

        private async Task HandleGenericExceptionAsync(HttpContext context, Exception exception)
        {
            _logger.LogError(exception, "Um erro inesperado (não tratado) ocorreu.");

            var problemDetails = new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "Ocorreu um erro inesperado no servidor.",
                Detail = "Por favor, entre em contato com o suporte."
            };

            context.Response.ContentType = "application/problem+json";
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            await context.Response.WriteAsync(JsonSerializer.Serialize(problemDetails));
        }
    }
}