using Microsoft.AspNetCore.Mvc.Rendering;

namespace HelpdeskSystem.ViewModels
{
    public class TechnicianPerformanceReportViewModel
    {
        // Filtros
        public int? CategoryId { get; set; }
        public string TechnicianId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        // Comboboxes
        public List<SelectListItem> Categories { get; set; }
        public List<SelectListItem> Technicians { get; set; }

        // Dados do relatório
        public List<TechnicianPerformanceViewModel> Results { get; set; }
    }

    public class TechnicianPerformanceViewModel
    {
        public string TechnicianName { get; set; }
        public int ResolvedCount { get; set; }
        public double AverageResolutionTimeHours { get; set; }
    }


}
