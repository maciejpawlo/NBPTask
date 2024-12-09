using System.Text.Json.Serialization;
using NBPTask.Domain.Clients.NBP.DTO;

namespace NBPTask.Domain.Clients.NBP.DTO;

public class ExchangeRateTableDto
{
    [JsonPropertyName("table")]
    public required string Type { get; set; }
    [JsonPropertyName("no")]
    public required string Number { get; set; }
    public DateTime EffectiveDate { get; set; }
    public IEnumerable<ExchangeRateDto> Rates { get; set; } = [];
}