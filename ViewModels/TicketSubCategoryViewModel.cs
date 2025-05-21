using HelpdeskSystem.Models.Ticket;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace HelpdeskSystem.ViewModels
{
    public class TicketSubCategoryViewModel
    {
        // Filtros
        public string Code { get; set; }
        public string Name { get; set; }
        public int CategoryId { get; set; }

        // Lista
        public List<TicketSubCategory> TicketSubCategories { get; set; }

        // Dropdowns
        public IEnumerable<SelectListItem> Categories { get; set; }
    }
}
