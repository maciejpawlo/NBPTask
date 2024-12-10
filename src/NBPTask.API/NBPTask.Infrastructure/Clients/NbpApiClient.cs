using System.Net.Http.Json;
using NBPTask.Domain.Clients.NBP;
using NBPTask.Domain.Clients.NBP.DTO;

namespace NBPTask.Infrastructure.Clients;

public class NbpApiClient(HttpClient httpClient) : INbpApiClient
{
    private readonly HttpClient _httpClient = httpClient;

    public async Task<IEnumerable<ExchangeRateTableDto>> GetLatestExchangeRates(string tableType, int topCount, CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.GetAsync($"exchangerates/tables/{tableType}/last/{topCount}?format=json", cancellationToken);
        response.EnsureSuccessStatusCode();
        var result = await response.Content.ReadFromJsonAsync<IEnumerable<ExchangeRateTableDto>>(cancellationToken) ?? [];
        return result;
    }
}