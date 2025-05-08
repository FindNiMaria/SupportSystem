using HelpdeskSystem.Models.User;
using System.ComponentModel;

namespace HelpdeskSystem.Models.System
{
    public class SystemCodeDetail : UserActivity
    {
        [DisplayName("ID")]
        public int Id { get; set; }

        [DisplayName("Código")]
        public string Code { get; set; }

        [DisplayName("Descrição")]
        public string Description { get; set; }

        [DisplayName("Ordem")]
        public int? OrderNo { get; set; }

        [DisplayName("Id de Código de Sistema")]
        public int SystemCodeId { get; set; }

        [DisplayName("Código de Sistema")]
        public SystemCode SystemCode { get; set; }
        
    }
}
