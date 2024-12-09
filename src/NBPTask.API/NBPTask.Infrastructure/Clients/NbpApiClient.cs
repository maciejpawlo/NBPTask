using System.Net.Http.Json;
using NBPTask.Domain.Clients.NBP;
using NBPTask.Domain.Clients.NBP.DTO;

namespace NBPTask.Infrastructure.Clients;

public class NbpApiClient(HttpClient httpClient) : INbpApiClient
{
    private readonly HttpClient _httpClient = httpClient;

    public async Task<IEnumerable<ExchangeRateTableDto>> GetLatestExchangeRates(string tableType, int topCount)
    {
        var response = await _httpClient.GetAsync($"exchangerates/tables/{tableType}/last/{topCount}?format=json");
        response.EnsureSuccessStatusCode();
        var result = await response.Content.ReadFromJsonAsync<IEnumerable<ExchangeRateTableDto>>() ?? [];
        return result;
    }
}