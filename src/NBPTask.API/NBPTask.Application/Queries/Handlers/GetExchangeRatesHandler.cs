using NBPTask.Application.DTO;
using NBPTask.Domain.Clients.NBP;
using NBPTask.Shared.Queries;
using NBPTask.Shared.Results;

namespace NBPTask.Application.Queries.Handlers;

public class GetExchangeRatesHandler(INbpApiClient nbpApiClient)
    : IQueryHandler<GetExchangeRates, Result<IReadOnlyCollection<ExchangeRateDto>>>
{
    private readonly INbpApiClient _nbpApiClient = nbpApiClient;

    public async Task<Result<IReadOnlyCollection<ExchangeRateDto>>> HandleAsync(GetExchangeRates query, CancellationToken cancellationToken)
    {
        var result = await _nbpApiClient.GetLatestExchangeRates(query.TableType, query.TopCount, cancellationToken);
        return result
            .SelectMany(x => x.Rates, (p, c) => new {p.EffectiveDate, c})
            .Select(x => new ExchangeRateDto
            {
                Currency = x.c.Currency,
                Code = x.c.Code,
                Mid = x.c.Mid,
                EffectiveDate = x.EffectiveDate
            })
            .ToList();
    }
}