using System.ComponentModel;

namespace HelpdeskSystem.Models.User
{
    public class UserActivity
    {
        [DisplayName("Criado por")]
        public string CreatedById { get; set; }

        [DisplayName("Criado por")]
        public ApplicationUser CreatedBy { get; set; }

        [DisplayName("Criado em")]
        public DateTime CreatedOn { get; set; }

        [DisplayName("Modificado por")]
        public string? ModifiedById { get; set; }

        [DisplayName("MOdificado por")]
        public ApplicationUser ModifiedBy { get; set; }

        [DisplayName("Modificado em")]
        public DateTime? ModifiedOn { get; set; }
    }
}
