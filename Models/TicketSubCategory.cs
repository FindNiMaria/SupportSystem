namespace HelpdeskSystem.Models
{
    public class TicketSubCategory : UserActivity
    {
        public  int Id { get; set; }
        public int CategoriaId { get; set; }
        public TicketCategory Categoria { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }

    }
}
