using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace BuildingBlocks.Behaviors
{
    public class LoggingBehavior<TRequest, TResponse>(
        ILogger<LoggingBehavior<TRequest, TResponse>> logger)
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : notnull
    {
        private const int PerformanceThresholdMs = 4000;

        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            var requestName = typeof(TRequest).Name;

            // 🔹 Log Request Start
            logger.LogInformation(
                "Handling {RequestName} with data: {@Request}",
                requestName,
                request);

            var stopwatch = Stopwatch.StartNew();

            try
            {
                var response = await next();

                stopwatch.Stop();
                var elapsedMs = stopwatch.ElapsedMilliseconds;

                // 🔹 Log Success
                logger.LogInformation(
                    "Handled {RequestName} in {ElapsedMilliseconds} ms",
                    requestName,
                    elapsedMs);

                // 🔥 Performance Warning
                if (elapsedMs > PerformanceThresholdMs)
                {
                    logger.LogWarning(
                        "⚠️ Performance issue: {RequestName} took {ElapsedMilliseconds} ms (>{Threshold} ms)",
                        requestName,
                        elapsedMs,
                        PerformanceThresholdMs);
                }

                return response;
            }
            catch (Exception ex)
            {
                stopwatch.Stop();

                logger.LogError(ex,
                    "Error in {RequestName} after {ElapsedMilliseconds} ms",
                    requestName,
                    stopwatch.ElapsedMilliseconds);

                throw; // let IExceptionHandler handle it
            }
        }
    }
}
