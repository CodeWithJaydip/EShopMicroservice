using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BuildingBlocks.Exceptions.Handler
{


    public class CustomExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<CustomExceptionHandler> _logger;

        public CustomExceptionHandler(ILogger<CustomExceptionHandler> logger)
        {
            _logger = logger;
        }

        public async ValueTask<bool> TryHandleAsync(
            HttpContext context,
            Exception exception,
            CancellationToken cancellationToken)
        {
            _logger.LogError(exception, exception.Message);

            var problemDetails = new ProblemDetails
            {
                Instance = context.Request.Path
            };

            switch (exception)
            {
                case AppException appEx:
                    problemDetails.Title = appEx.Message;
                    problemDetails.Status = appEx.StatusCode;
                    problemDetails.Extensions["errorCode"] = appEx.ErrorCode;
                    break;

                case FluentValidation.ValidationException valEx:
                    problemDetails.Title = "Validation failed";
                    problemDetails.Status = StatusCodes.Status400BadRequest;

                    var errors = valEx.Errors
                        .GroupBy(x => x.PropertyName)
                        .ToDictionary(
                            g => g.Key,
                            g => g.Select(x => x.ErrorMessage).ToArray()
                        );

                    problemDetails.Extensions["errors"] = errors;
                    break;

                default:
                    problemDetails.Title = "An unexpected error occurred";
                    problemDetails.Status = StatusCodes.Status500InternalServerError;
                    break;
            }

            context.Response.StatusCode = problemDetails.Status ?? 500;

            await context.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

            return true;
        }
    }
}
