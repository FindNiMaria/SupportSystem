using HelpdeskSystem.Models.Ticket;
using HelpdeskSystem.Models.User;
using System.ComponentModel;

namespace HelpdeskSystem.ViewModels
{
    public class TicketSubCategoriesVM : UserActivity
    {
        [DisplayName("Nº")]
        public int Id { get; set; }
        [DisplayName("Categoria")]
        public int CategoryId { get; set; }

        [DisplayName("Categoria")]
        public TicketCategory Category { get; set; }

        [DisplayName("Código")]
        public string Code { get; set; }

        [DisplayName("Nome")]
        public string Name { get; set; }

        public List<TicketSubCategory> TicketSubCategories { get; set; }
    }
}
