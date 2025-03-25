namespace HelpdeskSystem.Models
{
    public class Ticket
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public string Status { get; set; }
        public string Prioridade { get; set;}
        public string CriadoPorId { get; set; }
        public ApplicationUser CriadoPor { get; set; }
        public DateTime CriadoEm { get; set; }

    }
}
