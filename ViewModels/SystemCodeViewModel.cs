using HelpdeskSystem.Models.System;
using System.Collections.Generic;

namespace HelpdeskSystem.ViewModels
{
    public class SystemCodeViewModel
    {
        // Filtros
        public string Code { get; set; }
        public string Description { get; set; }

        // Lista
        public List<SystemCode> Codes { get; set; }
    }
}
