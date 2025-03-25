namespace HelpdeskSystem.Models
{
    public class UserActivity
    {
        public string CriadoPorId { get; set; }
        public ApplicationUser CriadoPor { get; set; }
        public DateTime CriadoEm { get; set; }
        public string? ModificadoPorId { get; set; }

        public ApplicationUser ModificadoPor { get; set; }
        public DateTime? ModificadoEm { get; set; }
    }
}
