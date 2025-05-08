using HelpdeskSystem.Models.User;

namespace HelpdeskSystem.Models.System
{
    public class SystemTask : UserActivity
    {
        public int Id { get; set; }
        public int? ParentId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public SystemTask Parent { get; set; }

        public ICollection<SystemTask>ChildTask { get; }
        public int? OrderNo { get; set; }
    }
}
