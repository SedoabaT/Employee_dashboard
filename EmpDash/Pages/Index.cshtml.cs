using EmpDash.Pages.Data;
using EmpDash.Pages.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace EmpDash.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly AppDbContext _context;

        public IndexModel(AppDbContext context, ILogger<IndexModel> logger)
        {
            _context = context;
            _logger = logger;
        }


        [BindProperty]
        public string Username { get; set; }
        public List<Tickets> Tickets { get; set; } = new List<Tickets>(); // Change from a single object to a list



        public async Task<IActionResult> OnGet()
        {

            try
            {
                string userEmail = HttpContext.Session.GetString("UserEmail");
                if (string.IsNullOrEmpty(userEmail))
                {
                    return RedirectToPage("/LoginP");
                }

                var user = await _context.users.FirstOrDefaultAsync(u => u.Email == userEmail);
                if (user != null)
                {

                    Username = user.FirstName;
                   
                }

                //diplay the ticket created by the user
                Tickets = await _context.tickets
                  .Where(t => t.CreatedBy == userEmail)
                  .OrderByDescending(t => t.CreatedDate) // Sort by latest tickets first
                  .ToListAsync();

                return Page();

            }
            catch (Exception ex)
            {
                //Console.WriteLine($"Error during login: {ex.Message}");
                ModelState.AddModelError(string.Empty, "An error occurred while processing your request.");
                return Page();
            }
        }

        //public void OnPost()
        //{
        //    HttpContext.Session.Clear();
        //    Response.Redirect("/LoginP");
        //}
    }
}
