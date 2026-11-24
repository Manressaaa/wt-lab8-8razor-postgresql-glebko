using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using wt_lab8_8razor_postgresql_glebko.Data;
using wt_lab8_8razor_postgresql_glebko.Models;

namespace wt_lab8_8razor_postgresql_glebko.Pages.Flights;

public class CreateModel : PageModel
{
    private readonly AppDbContext _context;

    public CreateModel(AppDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public Flight Flight { get; set; } = new();

    public IActionResult OnGet()
    {
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        // База данных сама сгенерирует ID (так как у нас Identity колонка),
        // поэтому вручную генерировать номер рейса не нужно!

        _context.Flights.Add(Flight);
        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}