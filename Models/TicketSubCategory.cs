namespace HelpdeskSystem.Models
{
    public class TicketSubCategory : UserActivity
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public TicketCategory Category { get; set; }
        public String Codigo { get; set; }
        public String Nome { get; set; }
    }
}
