using HelpdeskSystem.Models;
using System.ComponentModel;

namespace HelpdeskSystem.ViewModels
{
    public class TicketViewModel
    {
        [DisplayName("Nº")]
        public int Id { get; set; }

        [DisplayName("Título")]
        public string Titulo { get; set; }

        [DisplayName("Descrição")]
        public string Descricao { get; set; }

        [DisplayName("Status")]
        public string Status { get; set; }

        [DisplayName("Prioridade")]
        public string Prioridade { get; set; }

        [DisplayName("Criado por:")]
        public string CriadoPorId { get; set; }
        public ApplicationUser CriadoPor { get; set; }

        [DisplayName("Criado Em:")]
        public DateTime CriadoEm { get; set; }
        public int? IdSubCategoria { get; set; }

        [DisplayName("Categoria")]
        public int CategoriaId { get; set; }
        public TicketCategory Categoria { get; set; }

        [DisplayName("Sub-Categoria")]
        public int SubCategoriaId { get; set; }
        public TicketSubCategory SubCategoria { get; set; }

        public List<Ticket> Tickets { get; set; }
    }
}
