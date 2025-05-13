using HelpdeskSystem.Models.User;

namespace HelpdeskSystem.Models.SO
{
    public class OSComment : UserActivity
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int OSId { get; set; }
        public OS OS { get; set; }

    }
}
