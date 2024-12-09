using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace GeneralUsing.Exceptions
{
    public class CustomExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<CustomExceptionHandler> _logger;

        public CustomExceptionHandler(ILogger<CustomExceptionHandler> logger)
        {
            _logger = logger;
        }

        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            _logger.LogError(exception, "Error message: {exceptionMessage}, Time of occurrence: {time}", exception.Message, DateTime.UtcNow);

            (string Detail, string Title, int StatusCode) = exception switch
            {
                _ =>
                (
                exception.Message,
                exception.GetType().Name,
                httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError
                )
            };

            var problemDetails = new ProblemDetails
            {
                Title = Title,
                Detail = Detail,
                Status = StatusCode,
                Instance = httpContext.Request.Path
            };

            problemDetails.Extensions.Add("traceId", httpContext.TraceIdentifier);

            await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken: cancellationToken);
            return true;
        }
    }
}