using System.ComponentModel;

namespace HelpdeskSystem.ViewModels
{
    public class TicketOsViewModel
    {
        [DisplayName("ID")]
        public int Id { get; set; }

        [DisplayName("Título")]
        public string Title { get; set; }

        [DisplayName("Tipo")]
        public string Type { get; set; }

        [DisplayName("Status")]
        public string Status { get; set; }

        [DisplayName("Criado por")]
        public string CriadoPor { get; set; }

        [DisplayName("Código de Status")]
        public string StatusCode { get; set; }

        [DisplayName("Status")]
        public string StatusSystemCode { get; set; }

        [DisplayName("Criado em")]
        public DateTime CreatedOn { get; set; }

        [DisplayName("Atribuido a")]
        public string? AssignedToName { get; set; }
    }
}
