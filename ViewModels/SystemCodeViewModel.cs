using HelpdeskSystem.Models.System;
using System.Collections.Generic;
using System.ComponentModel;

namespace HelpdeskSystem.ViewModels
{
    public class SystemCodeViewModel
    {
        // Filtros
        [DisplayName("Código")]
        public string Code { get; set; }

        [DisplayName("Descrição")]
        public string Description { get; set; }

        // Lista
        public List<SystemCode> Codes { get; set; }
    }
}
