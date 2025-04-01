using HelpdeskSystem.Models;

namespace HelpdeskSystem.ViewModels
{
    public class TicketSubCategoriesVM : UserActivity
    {
        public int Id { get; set; }
        public int CategoriaId { get; set; }
        public TicketCategory Category { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }

        public List<TicketSubCategory> TicketSubCategories { get; set; }
    }
}
