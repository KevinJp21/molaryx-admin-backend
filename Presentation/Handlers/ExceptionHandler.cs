using Domain.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Shared.Common;
using System.Net;

namespace Presentation.Handlers
{
    public class ExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<ExceptionHandler> _logger;
        private readonly IHostEnvironment _hostEnvironment;

        public ExceptionHandler(
            ILogger<ExceptionHandler> logger,
            IHostEnvironment hostEnvironment)
        {
            _logger = logger;
            _hostEnvironment = hostEnvironment;
        }

        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext,
            Exception exception,
            CancellationToken cancellationToken)
        {
            LogException(exception, httpContext);

            var handledException = exception;

            while (handledException.InnerException != null)
            {
                handledException = handledException.InnerException;
            }

            TryHandleException(
                handledException,
                out var response
            );

            httpContext.Response.StatusCode =
                (int)response.HttpStatusCode;

            await httpContext.Response.WriteAsJsonAsync(
                response,
                cancellationToken
            );

            return true;
        }

        private void LogException(
            Exception exception,
            HttpContext context)
        {
            _logger.LogError(
                exception,
                "{ExceptionType}: {Method} {Path} --- {Message}",
                exception.GetType().Name,
                context.Request.Method,
                context.Request.Path,
                exception.Message
            );
        }

        private void TryHandleException(
            Exception exception,
            out ApiResponse<object> response)
        {
            response = new ApiResponse<object>
            {
                Ok = false,
                Data = null,
                Errors = new List<string>(),
                HttpStatusCode = HttpStatusCode.InternalServerError
            };

            switch (exception)
            {
                case CustomValidationException ex:
                    response.HttpStatusCode = HttpStatusCode.BadRequest;
                    response.Message = ex.Message;
                    response.Errors = ex.Errors;
                    break;

                case InvalidCredentialsException ex:
                    response.HttpStatusCode = HttpStatusCode.Unauthorized;
                    response.Message = ex.Message;
                    break;

                case UnauthorizedAccessException ex:
                    response.HttpStatusCode = HttpStatusCode.Unauthorized;

                    response.Message = ex.Message;
                    break;

                case AlreadyExistException ex:
                    response.HttpStatusCode = HttpStatusCode.Conflict;
                    response.Message = ex.Message;
                    break;

                case NotFoundException ex:
                    response.HttpStatusCode = HttpStatusCode.NotFound;
                    response.Message = ex.Message;
                    break;

                case InvalidOperationException ex:
                    response.HttpStatusCode = HttpStatusCode.BadRequest;
                    response.Message = ex.Message;
                    break;

                default:
                    response.Message =
                        "Error interno en el servidor.";

                    if (_hostEnvironment.IsDevelopment())
                    {
                        response.Errors = new List<string>
                        {
                            exception.Message
                        };
                    }

                    break;
            }
        }
    }
}