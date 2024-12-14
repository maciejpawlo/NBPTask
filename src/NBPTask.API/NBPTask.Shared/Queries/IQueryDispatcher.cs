namespace NBPTask.Shared.Queries;

public interface IQueryDispatcher
{
    Task<TResult> QueryAsync<TResult>(IQuery<TResult> query, CancellationToken token = default);
}