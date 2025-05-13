using HelpdeskSystem.Models.User;

namespace HelpdeskSystem.Models.SO
{
    public class OSSubCategory : UserActivity
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public OSCategory Category { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }

    }
}
