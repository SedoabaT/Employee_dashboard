using EmpDash.Pages.Data;
using EmpDash.Pages.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace EmpDash.Pages
{
    public class LoginPModel : PageModel
    {
        private readonly AppDbContext _context;

        public LoginPModel(AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public string Email { get; set; }

        [BindProperty]
        public string Password { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                var user = await _context.users.FirstOrDefaultAsync(u => u.Email == Email);

                if (user == null)
                {
                    ModelState.AddModelError(string.Empty, "Invalid email or password.");
                    return Page();
                }

                if (!VerifyPassword(Password, user.Password))
                {
                    ModelState.AddModelError(string.Empty, "Invalid email or password.");
                    return Page();
                }

                HttpContext.Session.SetString("UserEmail", user.Email);
                Console.WriteLine($"User session set: {user.Email}");
                return RedirectToPage("/Index");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during login: {ex.Message}");
                ModelState.AddModelError(string.Empty, "An error occurred while processing your request.");
                return Page();
            }
        }

        private bool VerifyPassword(string enteredPassword, string storedHashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(enteredPassword, storedHashedPassword);
        }
    }
}
