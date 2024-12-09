namespace NBPTask.Domain.Clients.NBP.DTO;

public class ExchangeRateDto
{
    public required string Currency { get; set; }
    public required string Code { get; set; }
    public required decimal Mid { get; set; }
}