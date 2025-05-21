using HelpdeskSystem.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

public class TicketIndexViewModel
{
    public int? CategoryId { get; set; }
    public int? PriorityId { get; set; }
    public int? StatusId { get; set; }
    public int? TechnicianId { get; set; }

    public List<TicketViewModel> Tickets { get; set; }

    public IEnumerable<SelectListItem> Categories { get; set; }
    public IEnumerable<SelectListItem> Priorities { get; set; }
    public IEnumerable<SelectListItem> Statuses { get; set; }
    public IEnumerable<SelectListItem> Technicians { get; set; }
}