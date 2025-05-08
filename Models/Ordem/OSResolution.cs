using HelpdeskSystem.Models.System;
using HelpdeskSystem.Models.User;
using Org.BouncyCastle.Bcpg.OpenPgp;

namespace HelpdeskSystem.Models.OS
{
    public class OSResolution : UserActivity
    {
        public int Id { get; set; }
        public int OSId { get; set; }
        public OS OS { get; set; }
        public string Description { get; set; }
        public int StatusId { get; set; }
        public SystemCodeDetail Status { get; set; }


    }
}
