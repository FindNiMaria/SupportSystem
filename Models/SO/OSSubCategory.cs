using HelpdeskSystem.Models.User;
using System.ComponentModel;

namespace HelpdeskSystem.Models.SO
{
    public class OSSubCategory : UserActivity
    {
        [DisplayName("Nº")]
        public int Id { get; set; }
        [DisplayName("Categoria")]
        public int CategoryId { get; set; }
        [DisplayName("Categoria")]
        public OSCategory Category { get; set; }
        [DisplayName("Código")]
        public string Code { get; set; }
        [DisplayName("Nome")]
        public string Name { get; set; }

    }
}
