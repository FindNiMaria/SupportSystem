using System.ComponentModel;

namespace HelpdeskSystem.Models
{
    public class Ticket : UserActivity
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

        [DisplayName ("Categoria: ")]
        public int CategoriaId { get; set; }
        public TicketCategory Categoria { get; set; }

        [DisplayName("Sub-Categoria: ")]
        public int? SubCategoryId { get; set; }
        public TicketSubCategory SubCategory { get; set;}

        [DisplayName("Anexo: ")]
        public String? Anexo { get; set; }

        public ICollection<Comment> TicketComments { get; set; }
    }
}
