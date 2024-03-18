using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace GyllenRates.Services;

public class CurrencyConversionService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<CurrencyConversionService> _logger;
    private readonly string _apiKey = "0f83a696628f79cf19a33a2c";

    public CurrencyConversionService(HttpClient httpClient, ILogger<CurrencyConversionService> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }

    public async Task<ConversionResult> ConvertCurrency(string baseCode, string targetCode, decimal amount)
    {
        var amountString = amount.ToString(CultureInfo.InvariantCulture);
        string requestUri = $"https://v6.exchangerate-api.com/v6/{_apiKey}/pair/{baseCode}/{targetCode}/{amountString}";
        var response = await _httpClient.GetAsync(requestUri);
        var responseContent = await response.Content.ReadAsStringAsync();

        if (response.IsSuccessStatusCode)
        {
            var conversionResult = JsonSerializer.Deserialize<ConversionResult>(responseContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return conversionResult;
        }
        else
        {
            _logger.LogError("Failed to get conversion rate. Status code: {StatusCode}", response.StatusCode);
            return null;
        }
    }
}

public class ConversionResult
{
    public string Result { get; set; }
    public string Documentation { get; set; }
    public string TermsOfUse { get; set; }
    public long TimeLastUpdateUnix { get; set; }
    public string TimeLastUpdateUtc { get; set; }
    public long TimeNextUpdateUnix { get; set; }
    public string TimeNextUpdateUtc { get; set; }
    public string BaseCode { get; set; }
    public string TargetCode { get; set; }
    public decimal Amount { get; set; }
    [JsonPropertyName("conversion_rate")]
    public decimal ConversionRate { get; set; }
    [JsonPropertyName("conversion_result")]
    public decimal ConvertedAmount { get; set; }
    public DateTime ConversionDate { get; set; } = DateTime.Now;
}