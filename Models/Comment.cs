namespace HelpdeskSystem.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public int IdChamado { get; set; }
        public Ticket Ticket { get; set; }
        public DateTime CriadoEm { get; set; }
        public string CriadoPorId { get; set; }
        public ApplicationUser CriadoPor { get; set; }

    }
}
