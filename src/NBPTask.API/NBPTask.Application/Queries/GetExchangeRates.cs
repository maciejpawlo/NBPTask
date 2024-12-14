using NBPTask.Application.DTO;
using NBPTask.Shared.Queries;
using NBPTask.Shared.Results;

namespace NBPTask.Application.Queries;

public record GetExchangeRates(string TableType, int TopCount) : IQuery<Result<IReadOnlyCollection<ExchangeRateDto>>>;