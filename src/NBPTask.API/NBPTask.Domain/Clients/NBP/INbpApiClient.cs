using NBPTask.Domain.Clients.NBP.DTO;

namespace NBPTask.Domain.Clients.NBP;

public interface INbpApiClient
{
    Task<IEnumerable<ExchangeRateTableDto>> GetLatestExchangeRates(string tableType, int topCount);
}