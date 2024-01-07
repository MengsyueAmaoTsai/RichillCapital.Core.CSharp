using System.Diagnostics;
using System.Reflection;

using MediatR;

using Microsoft.Extensions.Logging;

namespace RichillCapital.Core.SharedKernel;

public sealed class LoggingPipelineBehavior<TRequest, TResponse> :
    IPipelineBehavior<TRequest, TResponse>
    where TRequest : class, IRequest<TResponse>
    where TResponse : class
{
    private readonly ILogger<Mediator> _logger;

    public LoggingPipelineBehavior(ILogger<Mediator> logger) =>
        _logger = logger;

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var requestName = typeof(TRequest).Name;

        try
        {
            if (_logger.IsEnabled(LogLevel.Information))
            {
                _logger.LogInformation("Handling {RequestName}", requestName);

                var requestType = request.GetType();
                var properties = new List<PropertyInfo>(requestType.GetProperties());

                foreach (var property in properties)
                {
                    object? value = property.GetValue(request, null);

                    _logger.LogInformation("Property {Property} : {@Value}", property.Name, value);
                }
            }

            var stopwatch = Stopwatch.StartNew();

            var response = await next();

            _logger.LogInformation(
                "Handled {RequestName} with {Response} in {ms} ms",
                typeof(TRequest).Name,
                response,
                stopwatch.ElapsedMilliseconds);

            stopwatch.Stop();

            return response;
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "{RequestName} processing failed.", requestName);

            throw;
        }
    }
}