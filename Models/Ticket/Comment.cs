﻿using HelpdeskSystem.Models.User;

namespace HelpdeskSystem.Models.Ticket
{
    public class Comment : UserActivity
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int TicketId { get; set; }
        public Ticket Ticket { get; set; }

    }
}
