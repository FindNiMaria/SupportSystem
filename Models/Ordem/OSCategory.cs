using HelpdeskSystem.Models.System;
using HelpdeskSystem.Models.User;

namespace HelpdeskSystem.Models.OS
{
    public class OSCategory : UserActivity
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public int? DefaultPriorityId { get; set; }
        public SystemCodeDetail DefautPriority { get; set; }
    }
}
