using System.ComponentModel;

namespace HelpdeskSystem.Models.User
{
    public class AuditTrail
    {
        [DisplayName("Nº")]
        public int Id {get; set;}

        [DisplayName("Ação")]
        public string Action { get; set; }

        [DisplayName("Módulo")]
        public string Module { get; set; }

        [DisplayName("Tabela Afetada")]
        public string AffectedTable { get; set; }

        [DisplayName("Data")]
        public DateTime TimeStamp { get; set; } = DateTime.Now;

        [DisplayName("IP")]
        public string IpAdress { get; set; }

        [DisplayName("Usuário")]
        public string UserId { get; set; }

        [DisplayName("Usuário")]
        public ApplicationUser User { get; set; }


    }
}
