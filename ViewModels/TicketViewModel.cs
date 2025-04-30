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
        public int StatusId { get; set; }
        public SystemCodeDetail Status { get; set; }

        [DisplayName("Prioridade")]
        public int PrioridadeId { get; set; }
        public SystemCodeDetail Prioridade { get; set; }

        [DisplayName("Criado por:")]
        public string CriadoPorId { get; set; }
        public ApplicationUser CriadoPor { get; set; }

        [DisplayName("Criado Em:")]
        public DateTime CriadoEm { get; set; }

        [DisplayName("Categoria")]
        public int CategoriaId { get; set; }

        [DisplayName("Sub-Categoria")]
        public int SubCategoriaId { get; set; }
        public TicketSubCategory SubCategoria { get; set; }

        [DisplayName("Anexo: ")]
        public String Anexo { get; set; }
        public List<Ticket> Tickets { get; set; }
        public Ticket DetalhesChamado { get; set; }
        public List<Comment> ObservacoesChamado { get; set; }
        public Comment ObservacaoChamado { get; set; }
        public string DescricaoComentario { get; set; }
    }
}
