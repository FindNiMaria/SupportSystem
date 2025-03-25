namespace HelpdeskSystem.Models
{
    public class OSComments
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public int IdOS { get; set; }
        public OS OS { get; set; }
        public DateTime CriadoEm { get; set; }
        public string CriadoPorId { get; set; }
        public ApplicationUser CriadoPor { get; set; }
    }
}
