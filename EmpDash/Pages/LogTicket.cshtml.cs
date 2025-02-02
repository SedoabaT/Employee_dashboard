using EmpDash.Pages.Data;
using EmpDash.Pages.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace EmpDash.Pages
{
    public class LogTicketModel : PageModel
    {
        private readonly AppDbContext _context;

        public LogTicketModel(AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Tickets Ticket { get; set; }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            Console.WriteLine("OnPostAsync triggered.");

            // Retrieve session value before assigning it
            string userEmail = HttpContext.Session.GetString("UserEmail");
            Console.WriteLine($"Retrieved UserEmail from session: {userEmail}");

            Ticket.CreatedBy = userEmail;  // Assign the session value

            if (string.IsNullOrEmpty(Ticket.CreatedBy))
            {
                Console.WriteLine("Session UserEmail is null or empty.");
            }
            if (!ModelState.IsValid)
            {
                Console.WriteLine("ModelState is invalid. Errors:");
                foreach (var error in ModelState)
                {
                    foreach (var err in error.Value.Errors)
                    {
                        Console.WriteLine($"{error.Key}: {err.ErrorMessage}");
                    }
                }
                return Page();
            }

            try
            {
                Console.WriteLine($"Checking for existing ticket: {Ticket.Title}");

                var existingTicket = await _context.tickets.FirstOrDefaultAsync(t => t.Title == Ticket.Title);
                if (existingTicket != null)
                {
                    Console.WriteLine("Ticket already exists.");
                    ModelState.AddModelError("Ticket.Title", "Ticket already exists.");
                    return Page();
                }
                // Set the CreatedDate and Status before saving
                Ticket.CreatedDate = DateTime.Now;
                Ticket.Status = "Open";

                Console.WriteLine($"Saving new ticket: {Ticket.Title}, Created By: {Ticket.CreatedBy}");
                _context.tickets.Add(Ticket);
                await _context.SaveChangesAsync();

                return RedirectToPage("/Index");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                ModelState.AddModelError(string.Empty, "An error occurred while processing your request.");
                return Page();
            }
        }
    }
}