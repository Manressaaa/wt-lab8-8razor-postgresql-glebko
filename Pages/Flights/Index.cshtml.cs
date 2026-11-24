using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using wt_lab8_8razor_postgresql_glebko.Data;
using wt_lab8_8razor_postgresql_glebko.Models;

namespace wt_lab8_8razor_postgresql_glebko.Pages.Flights;

public class IndexModel : PageModel
{
    private readonly AppDbContext _context;

    public IndexModel(AppDbContext context)
    {
        _context = context;
    }

    // Свойство, которое использует ваш Index.cshtml
    public IList<Flight> Flights { get; set; } = default!;

    [BindProperty(SupportsGet = true)]
    public string? Search { get; set; }

    public async Task OnGetAsync()
    {
        var query = _context.Flights.AsQueryable();

        // Поиск по городам (если ввели в строку поиска)
        if (!string.IsNullOrWhiteSpace(Search))
        {
            query = query.Where(p => p.gorod_prilet.Contains(Search) || p.gorod_vilet.Contains(Search));
        }

        Flights = await query.OrderBy(p => p.nomer_reisa).ToListAsync();
    }

    // Обработчик для удаления (если добавите кнопку удаления в Index.cshtml)
    public async Task<IActionResult> OnPostDeleteAsync(int id)
    {
        var flight = await _context.Flights.FindAsync(id);
        if (flight != null)
        {
            _context.Flights.Remove(flight);
            await _context.SaveChangesAsync();
        }
        return RedirectToPage("./Index");
    }
}