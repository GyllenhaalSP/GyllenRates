namespace GyllenRates.Models;

public class Currency
{
    public int Id { get; set; }
    public string Country { get; set; }
    public string CountryCode { get; set; }
    public string CurrencyName { get; set; }
    public string Code { get; set; }
    public string? FlagUrl { get; set; }
}

