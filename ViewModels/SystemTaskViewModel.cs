using HelpdeskSystem.Models.System;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace HelpdeskSystem.ViewModels
{
    public class SystemTaskViewModel
    {
        // Filtros
        public string Code { get; set; }
        public string Name { get; set; }
        public string CreatedById { get; set; }
        public DateTime? CreatedFrom { get; set; }
        public DateTime? CreatedTo { get; set; }

        // Lista de tarefas do sistema
        public List<SystemTask> Tasks { get; set; }

        // Dropdowns
        public IEnumerable<SelectListItem> Users { get; set; }
    }
}
