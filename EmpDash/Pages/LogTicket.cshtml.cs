using EmpDash.Pages.Data;
using EmpDash.Pages.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace EmpDash.Pages
{
    public class LogTicketModel : PageModel
    {
        private readonly AppDbContext _context;
        private readonly ILogger<LogTicketModel> _logger;

        public LogTicketModel(AppDbContext context, ILogger<LogTicketModel> logger)
        {
            _context = context;
            _logger = logger;
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

            // Ensure Ticket is initialized before setting properties
            if (Ticket == null)
            {
                Ticket = new Tickets();
            }

            Ticket.CreatedBy = userEmail;  // Assign the session value to CreatedBy

            if (string.IsNullOrEmpty(Ticket.CreatedBy))
            {
                Console.WriteLine("Session UserEmail is null or empty.");
            }

            // Ensure CreatedBy is set before validation
            ModelState.Clear();
            TryValidateModel(Ticket);

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
                // Set default values before saving
                Ticket.CreatedDate = DateTime.Now;
                Ticket.Status = "Open";

                Console.WriteLine($"Saving new ticket: {Ticket.Title}, Created By: {Ticket.CreatedBy}");
                _context.tickets.Add(Ticket);
                await _context.SaveChangesAsync();

                TempData["ShowSnackbar"] = true;  // Store snackbar flag

                return RedirectToPage("/LogTicket");
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
