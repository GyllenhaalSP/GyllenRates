using GyllenRates.Models;
using Microsoft.EntityFrameworkCore;

namespace GyllenRates.Data;

public class AppDbContext : DbContext
{
    public DbSet<Currency> Currencies { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=currency.db");
    }
}
