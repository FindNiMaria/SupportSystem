using Microsoft.AspNetCore.Mvc.Rendering;

namespace HelpdeskSystem.ViewModels
{
    public class RelatorioTicketsViewModel
    {
        // Filtros
        public int? CategoryId { get; set; }
        public int? PriorityId { get; set; }
        public int? StatusId { get; set; }
        public string? TechnicianId { get; set; }
        public DateTime? DataInicio { get; set; }
        public DateTime? DataFim { get; set; }

        // Combos
        public SelectList Categorias { get; set; } = new SelectList(new List<object>());
        public SelectList Prioridades { get; set; } = new SelectList(new List<object>());
        public SelectList Status { get; set; } = new SelectList(new List<object>());
        public SelectList Tecnicos { get; set; } = new SelectList(new List<object>());

        // Resultado
        public List<TecnicoRelatorioViewModel> Relatorio { get; set; } = new();
    }



}
