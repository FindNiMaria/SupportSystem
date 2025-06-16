using HelpdeskSystem.Models.User;
using System.ComponentModel;

namespace HelpdeskSystem.Models.System
{
    public class SystemTask : UserActivity
    {
        [DisplayName("Nº")]
        public int Id { get; set; }

        [DisplayName("N Id Paiº")]
        public int? ParentId { get; set; }

        [DisplayName("Código")]
        public string Code { get; set; }

        [DisplayName("Nome")]
        public string Name { get; set; }

        [DisplayName("Pai")]
        public SystemTask Parent { get; set; }

        public ICollection<SystemTask>ChildTask { get; }

        [DisplayName("Ordem")]
        public int? OrderNo { get; set; }
    }
}
