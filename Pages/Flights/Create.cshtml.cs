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

        // ВОТ ЭТА СТРОКА: Принудительно делаем время UTC
        Flight.vremya_vileta = DateTime.SpecifyKind(Flight.vremya_vileta, DateTimeKind.Utc);

        _context.Flights.Add(Flight);
        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}