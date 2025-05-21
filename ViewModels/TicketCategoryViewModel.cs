using HelpdeskSystem.Controllers.User;
using HelpdeskSystem.Models.Ticket;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace HelpdeskSystem.ViewModels
{
    public class TicketCategoryViewModel : UserActivity
    {
        // Filtros
        public string Code { get; set; }
        public string Name { get; set; }
        public int? DefaultPriorityId { get; set; }

        // Listagem
        public List<TicketCategory> Categories { get; set; }

        // Dropdowns
        public IEnumerable<SelectListItem> Priorities { get; set; }
    }
}
