using HelpdeskSystem.Models.System;
using HelpdeskSystem.Models.User;
using System.ComponentModel;

namespace HelpdeskSystem.Models.Ticket
{
    public class TicketCategory : UserActivity
    {
        [DisplayName("Id")]
        public int Id { get; set; }

        [DisplayName("Código")]
        public string Code { get; set; }

        [DisplayName("Nome")]
        public string Name { get; set; }

        [DisplayName("Prioridade Padrão")]
        public int? DefaultPriorityId { get; set; }

        [DisplayName("Prioridade Padrão")]
        public SystemCodeDetail DefautPriority { get; set; }
    }
}
