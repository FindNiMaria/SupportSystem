using HelpdeskSystem.Models.Ticket;
using HelpdeskSystem.Models.User;

namespace HelpdeskSystem.ViewModels
{
    public class TicketSubCategoriesVM : UserActivity
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public TicketCategory Category { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }

        public List<TicketSubCategory> TicketSubCategories { get; set; }
    }
}
