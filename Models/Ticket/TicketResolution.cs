using HelpdeskSystem.Models.System;
using HelpdeskSystem.Models.User;
using Org.BouncyCastle.Bcpg.OpenPgp;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HelpdeskSystem.Models.Ticket
{
    public class TicketResolution : UserActivity
    {
        [DisplayName("Nº")]
        public int Id { get; set; }

        [DisplayName("Chamado")]
        public int TicketId { get; set; }

        [DisplayName("Chamado")]
        public Ticket Ticket { get; set; }

        [DisplayName("Descrição")]
        [Required(ErrorMessage = "A descrição é obrigatória.")]
        public string Description { get; set; }

        [DisplayName("Status")]
        [Required(ErrorMessage = "O Status é obrigatório.")]
        public int StatusId { get; set; }
        [DisplayName("Status")]
        public SystemCodeDetail Status { get; set; }


    }
}
