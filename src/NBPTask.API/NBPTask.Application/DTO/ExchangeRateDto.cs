namespace NBPTask.Application.DTO;

public class ExchangeRateDto
{
    public required string Currency { get; set; }
    public required string Code { get; set; }
    public required decimal Mid { get; set; }
    public DateTime EffectiveDate { get; set; }
}