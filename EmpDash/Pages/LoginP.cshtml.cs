using EmpDash.Pages.Data;
using EmpDash.Pages.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

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
        public Users LoginInput { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }


        
    }
}
