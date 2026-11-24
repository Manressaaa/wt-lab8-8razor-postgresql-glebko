using Microsoft.EntityFrameworkCore;
using wt_lab8_8razor_postgresql_glebko.Models;

namespace wt_lab8_8razor_postgresql_glebko.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Flight> Flights => Set<Flight>();

    // Переносим данные из FlightStore.cs сюда
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Flight>().HasKey(f => f.nomer_reisa);

        modelBuilder.Entity<Flight>().HasData(
            new Flight
            {
                nomer_reisa = 101,
                gorod_vilet = "Нюйорк",
                gorod_prilet = "Горки)",
                vremya_vileta = new DateTime(2026, 11, 15, 8, 30, 0, DateTimeKind.Utc),
                cena = 250,
                mesta = 45,
                dostupno = true,
                CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc) 
            },
            new Flight
            {
                nomer_reisa = 102,
                gorod_vilet = "Минск",
                gorod_prilet = "Горки",
                vremya_vileta = new DateTime(2026, 11, 16, 12, 0, 0, DateTimeKind.Utc),
                cena = 180,
                mesta = 30,
                dostupno = true,
                CreatedAt = new DateTime(2026, 1, 2, 0, 0, 0, DateTimeKind.Utc)
            },
            new Flight
            {
                nomer_reisa = 103,
                gorod_vilet = "Москва",
                gorod_prilet = "Горки",
                vremya_vileta = new DateTime(2026, 11, 17, 22, 15, 0, DateTimeKind.Utc),
                cena = 400,
                mesta = 0,
                dostupno = false,
                CreatedAt = new DateTime(2026, 1, 3, 0, 0, 0, DateTimeKind.Utc)
            },
            new Flight
            {
                nomer_reisa = 104,
                gorod_vilet = "Гомель",
                gorod_prilet = "Горки",
                vremya_vileta = new DateTime(2026, 12, 20, 14, 0, 0, DateTimeKind.Utc),
                cena = 300,
                mesta = 50,
                dostupno = true,
                CreatedAt = new DateTime(2026, 1, 4, 0, 0, 0, DateTimeKind.Utc)
            },
            new Flight
            {
                nomer_reisa = 105,
                gorod_vilet = "Витебск",
                gorod_prilet = "Горки",
                vremya_vileta = new DateTime(2026, 12, 25, 6, 45, 0, DateTimeKind.Utc),
                cena = 150,
                mesta = 20,
                dostupno = true,
                CreatedAt = new DateTime(2026, 1, 5, 0, 0, 0, DateTimeKind.Utc)
            }
        );
    }
}