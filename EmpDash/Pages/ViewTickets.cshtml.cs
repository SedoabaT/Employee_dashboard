using EmpDash.Pages.Data;
using EmpDash.Pages.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace EmpDash.Pages
{
    public class ViewTicketsModel : PageModel
    {
        private readonly AppDbContext _context;

        public ViewTicketsModel(AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public List<Tickets> Tickets { get; set; } = new List<Tickets>();

        public async Task<IActionResult> OnGet()
        {
            try
            {
                string userEmail = HttpContext.Session.GetString("UserEmail");

                if (string.IsNullOrEmpty(userEmail))
                {

                    
                    return RedirectToPage("/LoginP");
                }

                Tickets = await _context.tickets
                    .Where(t => t.CreatedBy == userEmail)
                    .OrderByDescending(t => t.CreatedDate)
                    .ToListAsync();
                return Page();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "An error occurred while processing your request.");
                return Page();
            }
        }

    }
}
