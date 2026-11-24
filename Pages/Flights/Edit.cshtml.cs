using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using wt_lab8_8razor_postgresql_glebko.Data;
using wt_lab8_8razor_postgresql_glebko.Models;

namespace wt_lab8_8razor_postgresql_glebko.Pages.Flights;

public class EditModel : PageModel
{
    private readonly AppDbContext _context;

    public EditModel(AppDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public Flight Flight { get; set; } = new();

    public async Task<IActionResult> OnGetAsync(int id)
    {
        // Получаем рейс из базы данных
        var existing = await _context.Flights.FindAsync(id);
        if (existing == null)
        {
            return NotFound();
        }

        Flight = existing;
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        // Находим существующий рейс в базе
        var existing = await _context.Flights.FindAsync(Flight.nomer_reisa);
        if (existing == null)
        {
            return NotFound();
        }

        // Обновляем поля
        existing.gorod_vilet = Flight.gorod_vilet;
        existing.gorod_prilet = Flight.gorod_prilet;
        existing.vremya_vileta = Flight.vremya_vileta;
        existing.cena = Flight.cena;
        existing.mesta = Flight.mesta;
        existing.dostupno = Flight.dostupno;

        // Сохраняем изменения в базе данных!
        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}