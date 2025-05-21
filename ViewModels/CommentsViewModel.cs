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
        public int? TicketId { get; set; }
        public string CreatedById { get; set; }
        public string DescriptionKeyword { get; set; }
        public DateTime? CreatedFrom { get; set; }
        public DateTime? CreatedTo { get; set; }

        // Lista de comentários
        public List<Comment> Comments { get; set; }
        public int Id { get; set; }
        public string Description { get; set; }
        public Ticket Ticket { get; set; }
        // Dropdowns
        public IEnumerable<SelectListItem> Tickets { get; set; }
        public IEnumerable<SelectListItem> Users { get; set; }
    }
}
