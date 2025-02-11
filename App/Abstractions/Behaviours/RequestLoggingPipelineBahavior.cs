using Microsoft.Extensions.Logging;
using Serilog.Context;

namespace App.Abstractions.Behaviours;

/// <summary>
/// Implementacija pipeline ponašanja za logiranje zahtjeva.
/// Ova klasa bilježi informacije o obradi zahtjeva, uključujući uspješne i neuspješne rezultate.
/// </summary>
/// <typeparam name="TRequest"></typeparam>
/// <typeparam name="TResponse"></typeparam>
/// <param name="logger"></param>


internal sealed class RequestLoggingPipelineBahavior<TRequest, TResponse>(
    ILogger<RequestLoggingPipelineBahavior<TRequest, TResponse>> logger)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : class
    where TResponse : Result
{
    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        string requestName = typeof(TRequest).Name;

        logger.LogInformation("Processing request {RequestName}", requestName);

        TResponse result = await next();

        if (result.IsSuccess) 
        {
            logger.LogInformation("Completed request {RequestName}", requestName);
        }
        else
        {
            using (LogContext.PushProperty("Error", result.Error, true))
            {
                logger.LogError("Completed request {RequestName} with error", requestName);
            }
        }

        return result;
    }
}
