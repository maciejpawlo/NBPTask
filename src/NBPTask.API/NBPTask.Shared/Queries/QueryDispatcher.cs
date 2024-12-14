using Microsoft.Extensions.DependencyInjection;

namespace NBPTask.Shared.Queries;

public class QueryDispatcher(IServiceProvider serviceProvider) : IQueryDispatcher
{
    private readonly IServiceProvider _serviceProvider = serviceProvider;
    
    public async Task<TResult> QueryAsync<TResult>(IQuery<TResult> query, CancellationToken cancellationToken = default)
    {
        using var scope = _serviceProvider.CreateScope();
        var handlerType = typeof(IQueryHandler<,>).MakeGenericType(query.GetType(), typeof(TResult));
        var handler = scope.ServiceProvider.GetRequiredService(handlerType);

        return await (Task<TResult>)handlerType
            .GetMethod(nameof(IQueryHandler<IQuery<TResult>, TResult>.HandleAsync))
            ?.Invoke(handler, [query, cancellationToken])!;
    }
}