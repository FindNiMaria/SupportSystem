using HelpdeskSystem.Controllers.User;
using HelpdeskSystem.Models.System;
using System.Collections.Generic;

namespace HelpdeskSystem.ViewModels
{
    public class SystemSettingsViewModel:UserActivity
    {
        // Filtros
        public string Code { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }

        // Lista
        public List<SystemSettings> Settings { get; set; }
    }
}
