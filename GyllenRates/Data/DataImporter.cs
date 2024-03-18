namespace GyllenRates.Data;

using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using GyllenRates.Models;

public static class DataImporter
{
    public static void ImportCurrencies(string filePath)
    {
        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            HasHeaderRecord = true,
        };

        using var reader = new StreamReader(filePath);
        using var csv = new CsvReader(reader, config);
        csv.Context.RegisterClassMap<CurrencyMap>();
        var records = csv.GetRecords<Currency>().ToList();

        using var db = new AppDbContext();
        db.Database.EnsureCreated();

        foreach (var record in records)
        {
            if (!db.Currencies.Any(c => c.Code == record.Code))
            {
                db.Currencies.Add(record);
            }
        }

        db.SaveChanges();
    }

    public static void UpdateFlagUrls()
    {
        using var db = new AppDbContext();
        var currencies = db.Currencies.Where(c => string.IsNullOrEmpty(c.FlagUrl)).ToList();

        foreach (var currency in currencies)
        {
            currency.FlagUrl = $"https://flagsapi.com/{currency.CountryCode}/flat/64.png";
        }

        db.SaveChanges();
    }
}

public sealed class CurrencyMap : ClassMap<Currency>
{
    public CurrencyMap()
    {
        Map(m => m.Country).Name("Country");
        Map(m => m.CountryCode).Name("CountryCode");
        Map(m => m.CurrencyName).Name("Currency");
        Map(m => m.Code).Name("Code");
    }
}


