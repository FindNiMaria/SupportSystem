using HelpdeskSystem.Models.User;
using System.ComponentModel;

namespace HelpdeskSystem.Models.Ticket
{
    public class TicketSubCategory : UserActivity
    {
        [DisplayName("ID")]
        public  int Id { get; set; }

        [DisplayName("Categoria")]
        public int CategoryId { get; set; }

        [DisplayName("Categoria")]
        public TicketCategory Category { get; set; }

        [DisplayName("Código")]
        public string Code { get; set; }

        [DisplayName("Nome")]
        public string Name { get; set; }

    }
}
