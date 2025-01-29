using EmpDash.Pages.Data;
using EmpDash.Pages.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace EmpDash.Pages
{
    public class RegistrationPModel : PageModel
    {
        private readonly AppDbContext _context;

        public RegistrationPModel (AppDbContext context) 
        {
            _context = context;

        }
        [BindProperty]
        public Users user { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) 
            {
            
                return Page();
            }

            var existingUser =  _context.users.FirstOrDefault(u => u.Email == user.Email);
            if (existingUser != null)
            {
                ModelState.AddModelError("User.Email", "Email already registered.");
                return Page();
            }

            // Add user to database
            _context.users.Add(user);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Login");

        }

    }
}
