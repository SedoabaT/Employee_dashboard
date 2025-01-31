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

        public RegistrationPModel(AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Users User { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
      
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                var existingUser = await _context.users.FirstOrDefaultAsync(u => u.Email == User.Email);
                if (existingUser != null)
                {
                    ModelState.AddModelError("User.Email", "Email already registered.");
                    return Page();
                }

                // Hash password before saving (if needed)
                User.Password = HashPassword(User.Password);

                _context.users.Add(User);
                await _context.SaveChangesAsync();

                return RedirectToPage("/LoginP");
            }
            catch (Exception ex)
            {
                // Properly log the exception
                Console.WriteLine($"Error during registration: {ex.Message}");
                ModelState.AddModelError(string.Empty, "An error occurred while processing your request.");
                return Page();
            }
        }

        public static string HashPassword(string password)
        {
           return BCrypt.Net.BCrypt.HashPassword(password);
        }
    }
}
