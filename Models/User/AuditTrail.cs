﻿namespace HelpdeskSystem.Models.User
{
    public class AuditTrail
    {
        public int Id {get; set;}
        public string Action { get; set; }
        public string Module { get; set; }
        public string AffectedTable { get; set; }
        public DateTime TimeStamp { get; set; } = DateTime.Now;
        public string IpAdress { get; set; }
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }


    }
}
