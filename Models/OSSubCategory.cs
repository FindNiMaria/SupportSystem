namespace HelpdeskSystem.Models
{
    public class OSSubCategory : UserActivity
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public OSCategory Category { get; set; }
        public String Codigo { get; set; }
        public String Nome { get; set; }
    }
}
