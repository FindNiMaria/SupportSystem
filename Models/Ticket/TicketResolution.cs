﻿using HelpdeskSystem.Models.System;
using HelpdeskSystem.Models.User;
using Org.BouncyCastle.Bcpg.OpenPgp;

namespace HelpdeskSystem.Models.Ticket
{
    public class TicketResolution : UserActivity
    {
        public int Id { get; set; }
        public int TicketId { get; set; }
        public Ticket Ticket { get; set; }
        public string Description { get; set; }
        public int StatusId { get; set; }
        public SystemCodeDetail Status { get; set; }


    }
}
