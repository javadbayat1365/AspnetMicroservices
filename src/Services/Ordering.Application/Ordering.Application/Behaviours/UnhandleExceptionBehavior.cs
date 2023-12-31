using MediatR;
using Microsoft.Extensions.Logging;

namespace Ordering.Application.Behaviours;

public class UnhandleExceptionBehavior<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse>
{
	private readonly ILogger<TRequest> _logger;

    public UnhandleExceptionBehavior(ILogger<TRequest> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
		try
		{
			return await next();
		}
		catch (Exception ex)
		{
			var name = typeof(TRequest).Name;
			_logger.LogError(ex,$"Application Request: Unhandle Exception for request {name}");
			throw;
		}
    }
}
