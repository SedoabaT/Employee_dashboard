namespace EmpDash.Pages.Model
{
    public class Tickets
    {
         public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Status { get; set; } = "Open"; // Open, Closed
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public string Priority { get; set; } // Low, Medium, High
        public string CreatedBy { get; set; } // User email or ID
    }
}
