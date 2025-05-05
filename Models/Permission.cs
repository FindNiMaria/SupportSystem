using System.ComponentModel;

namespace HelpdeskSystem.Models
{
    public class Permission
    {
        [DisplayName("ID")]
        public int Id { get; set; }

        [DisplayName("Nome")]
        public string Name { get; set; }

        [DisplayName("Código")]
        public string Code { get; set; }

        [DisplayName("Descrição")]
        public string Description { get; set; }

        [DisplayName("Módulo")]
        public string Module { get; set; }
    }
}
