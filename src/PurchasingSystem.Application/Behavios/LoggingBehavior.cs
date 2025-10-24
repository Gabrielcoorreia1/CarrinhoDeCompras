using MediatR;
using Microsoft.Extensions.Logging;

namespace PurchasingSystem.Application.Behavios
{
    public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    {
        private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;

        public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var requestName = typeof(TRequest).Name;

            _logger.LogInformation("Iniciando requisição: {RequestName}", requestName);

            var response = await next();

            _logger.LogInformation("Requisição finalizada: {RequestName}", requestName);

            return response;
        }
    }
}
