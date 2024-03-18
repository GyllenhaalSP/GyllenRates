namespace GyllenRates.Models;

public class Conversion
{
    public Currency SourceCurrency { get; set; }
    public Currency TargetCurrency { get; set; }
    public decimal Amount { get; set; }
    public decimal ConvertedAmount { get; set; }
    public DateTime ConversionDate { get; set; }
}