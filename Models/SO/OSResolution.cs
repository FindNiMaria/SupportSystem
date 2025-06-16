using HelpdeskSystem.Models.System;
using HelpdeskSystem.Models.User;
using Org.BouncyCastle.Bcpg.OpenPgp;
using System.ComponentModel;

namespace HelpdeskSystem.Models.SO
{
    public class OSResolution : UserActivity
    {
        [DisplayName("Nº")]
        public int Id { get; set; }
        [DisplayName("OS Nº")]
        public int OSId { get; set; }
        [DisplayName("OS")]
        public OS OS { get; set; }
        [DisplayName("Descrição")]
        public string Description { get; set; }
        [DisplayName("Status")]
        public int StatusId { get; set; }
        [DisplayName("Status")]
        public SystemCodeDetail Status { get; set; }


    }
}
