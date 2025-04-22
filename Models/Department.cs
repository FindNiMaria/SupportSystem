using System.ComponentModel;

namespace HelpdeskSystem.Models
{
    public class Department : UserActivity
    {
        [DisplayName("Id do Departamento")]
        public int Id { get; set; }

        [DisplayName("Codigo do Departamento")]
        public string Codigo { get; set; }

        [DisplayName("Código do Departamento")]
        public string Nome { get; set; }
    }
}
