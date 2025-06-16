using HelpdeskSystem.Models.User;
using System.ComponentModel;

namespace HelpdeskSystem.Models.Ticket
{
    public class Comment : UserActivity
    {
        [DisplayName("Nº")]
        public int Id { get; set; }

        [DisplayName("Descrição")]
        public string Description { get; set; }

        [DisplayName("Chamado")]
        public int TicketId { get; set; }
        [DisplayName("Chamado")]
        public Ticket Ticket { get; set; }

    }
}
