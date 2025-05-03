using System.ComponentModel;

namespace HelpdeskSystem.Models
{
    public class Department : UserActivity
    {
        [DisplayName("Id do Departamento")]
        public int Id { get; set; }

        [DisplayName("Codigo do Departamento")]
        public string Code { get; set; }

        [DisplayName("Departamento")]
        public string Name { get; set; }
    }
}
