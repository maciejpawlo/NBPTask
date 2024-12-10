using NBPTask.Application.DTO;
using NBPTask.Shared.Queries;

namespace NBPTask.Application.Queries;

public record GetExchangeRates(string TableType, int TopCount) : IQuery<IReadOnlyCollection<ExchangeRateDto>>;