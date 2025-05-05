namespace HelpdeskSystem.Models
{
    public class SystemSettings :UserActivity
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
    }
}
