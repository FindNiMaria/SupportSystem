using HelpdeskSystem.Models.Ticket;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel;

namespace HelpdeskSystem.ViewModels
{
    public class TicketSubCategoryViewModel
    {
        // Filtros
        [DisplayName("Código")]
        public string Code { get; set; }

        [DisplayName("Nome")]
        public string Name { get; set; }

        [DisplayName("Categoria")]
        public int CategoryId { get; set; }

        // Lista
        public List<TicketSubCategory> TicketSubCategories { get; set; }

        // Dropdowns
        public IEnumerable<SelectListItem> Categories { get; set; }
    }
}
