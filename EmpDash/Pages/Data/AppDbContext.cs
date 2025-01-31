using System.Net.Sockets;
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Users>()
                .HasKey(u => u.EmployeeId); // Explicitly define the primary key

            modelBuilder.Entity<Tickets>()
                .HasKey(t => t.Id); // Explicitly define the primary key
        }
    }
   
    }
