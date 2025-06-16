using HelpdeskSystem.Models.System;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace HelpdeskSystem.ViewModels
{
    public class SystemTaskViewModel
    {
        // Filtros

        [DisplayName("Código")]
        public string Code { get; set; }

        [DisplayName("Nome")]
        public string Name { get; set; }

        [DisplayName("Criado por")]
        public string CreatedById { get; set; }

        [DisplayName("Criado de")]
        public DateTime? CreatedFrom { get; set; }

        [DisplayName("Até")]
        public DateTime? CreatedTo { get; set; }

        // Lista de tarefas do sistema
        public List<SystemTask> Tasks { get; set; }

        // Dropdowns
        public IEnumerable<SelectListItem> Users { get; set; }
    }
}
