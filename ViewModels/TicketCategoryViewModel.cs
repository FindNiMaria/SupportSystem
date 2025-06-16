using HelpdeskSystem.Controllers.User;
using HelpdeskSystem.Models.Ticket;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace HelpdeskSystem.ViewModels
{
    public class TicketCategoryViewModel : UserActivity
    {
        // Filtros
        [DisplayName("Código")]
        public string Code { get; set; }
        [DisplayName("Nome")]
        public string Name { get; set; }
        [DisplayName("Prioridade Padrão")]
        public int? DefaultPriorityId { get; set; }

        // Listagem
        public List<TicketCategory> Categories { get; set; }

        // Dropdowns
        public IEnumerable<SelectListItem> Priorities { get; set; }
    }
}
