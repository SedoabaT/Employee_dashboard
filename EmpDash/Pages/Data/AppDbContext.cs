using EmpDash.Pages.Model;
using Microsoft.EntityFrameworkCore;
namespace EmpDash.Pages.Data
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
        {
        }

        public DbSet<Users> users { get; set; }
        public DbSet<Tickets> tickets { get; set; }
    }
}
