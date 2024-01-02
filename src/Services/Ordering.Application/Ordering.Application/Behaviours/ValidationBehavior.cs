using FluentValidation;
using MediatR;
using ValidationException = Ordering.Application.Exceptions.ValidationException;


namespace Ordering.Application.Behaviours;

public class ValidationBehavior<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators ?? throw new ArgumentNullException(nameof(validators));
    }

    public async Task<TResponse> Handle(
        TRequest request, 
        RequestHandlerDelegate<TResponse> next,
        CancellationToken token)
    {
        if(_validators.Any())
        {
            var context = new ValidationContext<TRequest>(request);
            var validatonResults = await Task.WhenAll(_validators.Select(s => s.ValidateAsync(context,token)));
            var failurs = validatonResults.SelectMany(s => s.Errors).Where(w => w != null).ToList();

            if (failurs.Count != 0)
                throw new ValidationException(failurs);
        }
        return await next();
    }
}
