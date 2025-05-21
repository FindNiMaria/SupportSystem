namespace HelpdeskSystem.ViewModels
{
    public class TicketOsViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Type { get; set; } // "Ticket" ou "OS"
        public string Status { get; set; }
        public string CriadoPor { get; set; }
        public string StatusCode { get; set; }
        public string StatusSystemCode { get; set; }
        public DateTime CreatedOn { get; set; }
        public string? AssignedToName { get; set; }
    }
}
