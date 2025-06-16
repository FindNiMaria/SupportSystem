using HelpdeskSystem.Models.User;
using System.ComponentModel;

namespace HelpdeskSystem.Models.System
{
    public class SystemSettings :UserActivity
    {
        [DisplayName("Nº")]
        public int Id { get; set; }
        [DisplayName("Código")]
        public string Code { get; set; }
        [DisplayName("Nome")]
        public string Name { get; set; }
        [DisplayName("Valor")]
        public string Value { get; set; }
    }
}
