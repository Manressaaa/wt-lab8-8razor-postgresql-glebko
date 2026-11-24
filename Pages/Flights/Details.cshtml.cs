using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using wt_lab8_8razor_postgresql_glebko.Data;
using wt_lab8_8razor_postgresql_glebko.Models;

namespace wt_lab8_8razor_postgresql_glebko.Pages.Flights;

public class DetailsModel : PageModel
{
    private readonly AppDbContext _context;

    public DetailsModel(AppDbContext context)
    {
        _context = context;
    }

    public Flight? Flight { get; set; }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        // Ищем рейс в базе данных по ID
        Flight = await _context.Flights.FirstOrDefaultAsync(f => f.nomer_reisa == id);

        if (Flight == null)
        {
            return NotFound();
        }

        return Page();
    }
}