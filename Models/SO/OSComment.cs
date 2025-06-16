using HelpdeskSystem.Models.User;
using System.ComponentModel;

namespace HelpdeskSystem.Models.SO
{
    public class OSComment : UserActivity
    {
        [DisplayName("Nº")]
        public int Id { get; set; }

        [DisplayName("Descrição")]
        public string Description { get; set; }

        [DisplayName("OS Nº")]
        public int OSId { get; set; }

        [DisplayName("OS")]
        public OS OS { get; set; }

    }
}
