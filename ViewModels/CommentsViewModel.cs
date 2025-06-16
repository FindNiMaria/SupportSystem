using HelpdeskSystem.Models.Ticket;
using HelpdeskSystem.Models.User;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace HelpdeskSystem.ViewModels
{
    public class CommentsViewModel : UserActivity
    {
        // Filtros

        [DisplayName("Nº")]
        public int? TicketId { get; set; }

        [DisplayName("CriadoPor")]
        public string CreatedById { get; set; }

        [DisplayName("Palavra-Chave")]
        public string DescriptionKeyword { get; set; }

        [DisplayName("De")]
        public DateTime? CreatedFrom { get; set; }

        [DisplayName("Até")]
        public DateTime? CreatedTo { get; set; }

        // Lista de comentários
        public List<Comment> Comments { get; set; }

        [DisplayName("Nº")]
        public int Id { get; set; }

        [DisplayName("Descrição")]
        public string Description { get; set; }

        [DisplayName("Chamado")]
        public Ticket Ticket { get; set; }
        // Dropdowns
        public IEnumerable<SelectListItem> Tickets { get; set; }
        public IEnumerable<SelectListItem> Users { get; set; }
    }
}
