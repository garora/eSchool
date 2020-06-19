using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;

namespace OpenCodeFoundation.ESchool.Services.Enrolling.API.Application.Behaviors
{
    public class LoggingBehavior<TRequest, TResponse>
        : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;

        public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
        {
            _logger = logger ?? throw new System.ArgumentNullException(nameof(logger));
        }

        public async Task<TResponse> Handle(
            TRequest request,
            CancellationToken cancellationToken,
            RequestHandlerDelegate<TResponse> next)
        {
            _logger.LogDebug(
                "Handling command {CommandName} ({@Command})",
                request.GetType().Name,
                request);

            var response = await next();

            _logger.LogDebug(
                "Command {CommandName} handled. Response: {@Response}",
                request.GetType().Name,
                response);

            return response;
        }
    }
}
