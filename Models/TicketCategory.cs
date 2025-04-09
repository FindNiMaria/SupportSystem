namespace HelpdeskSystem.Models
{
    public class TicketCategory : UserActivity
    {
        public int Id { get; set; }
        public String Codigo { get; set; }
        public String Nome { get; set; }
        public int? PrioridadePadraoId { get; set; }
        public SystemCodeDetail PrioridadePadrao { get; set; }
    }
}
