using HelpdeskSystem.Controllers.User;
using HelpdeskSystem.Models.System;
using System.Collections.Generic;
using System.ComponentModel;

namespace HelpdeskSystem.ViewModels
{
    public class SystemSettingsViewModel:UserActivity
    {
        // Filtros

        [DisplayName("Código")]
        public string Code { get; set; }

        [DisplayName("Nome")]
        public string Name { get; set; }

        [DisplayName("Valor")]
        public string Value { get; set; }

        // Lista
        public List<SystemSettings> Settings { get; set; }
    }
}
