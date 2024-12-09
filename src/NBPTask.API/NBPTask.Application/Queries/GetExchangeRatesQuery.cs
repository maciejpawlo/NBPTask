using NBPTask.Application.DTO;
using NBPTask.Shared.Queries;

namespace NBPTask.Application.Queries;

public record GetExchangeRatesQuery(string TableType, int TopCount) : IQuery<IReadOnlyCollection<ExchangeRateDto>>;