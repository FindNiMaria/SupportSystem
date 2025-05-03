namespace HelpdeskSystem.Models
{
    public class TicketCategory : UserActivity
    {
        public int Id { get; set; }
        public String Code { get; set; }
        public String Name { get; set; }
        public int? DefaultPriorityId { get; set; }
        public SystemCodeDetail DefautPriority { get; set; }
    }
}
